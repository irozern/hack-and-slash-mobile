#!/usr/bin/env bash
set -euo pipefail

if ! command -v jq >/dev/null 2>&1; then
  echo "This script requires 'jq' (https://stedolan.github.io/jq/). Install and retry." >&2
  exit 2
fi

: "${GITHUB_TOKEN:?Set GITHUB_TOKEN environment variable with a PAT (repo/workflow scopes)}"

OWNER="irozern"
REPO="hack-and-slash-mobile"
WORKFLOW_FILE="unity-ci.yml"
REF="main"

echo "Dispatching workflow $WORKFLOW_FILE on $OWNER/$REPO#$REF..."
curl -s -X POST \
  -H "Authorization: token $GITHUB_TOKEN" \
  -H "Accept: application/vnd.github+json" \
  "https://api.github.com/repos/$OWNER/$REPO/actions/workflows/$WORKFLOW_FILE/dispatches" \
  -d "{\"ref\":\"$REF\"}" || true

echo "Waiting for workflow run to appear..."
sleep 3

RUN_ID=""
for i in {1..30}; do
  RUN_JSON=$(curl -s -H "Authorization: token $GITHUB_TOKEN" -H "Accept: application/vnd.github+json" "https://api.github.com/repos/$OWNER/$REPO/actions/workflows/$WORKFLOW_FILE/runs?branch=$REF&per_page=5")
  RUN_ID=$(echo "$RUN_JSON" | jq -r '.workflow_runs[0].id // empty')
  if [ -n "$RUN_ID" ]; then break; fi
  sleep 2
done

if [ -z "$RUN_ID" ]; then
  echo "Failed to detect workflow run. Exiting." >&2
  exit 3
fi

echo "Found workflow run id: $RUN_ID"

while :; do
  run=$(curl -s -H "Authorization: token $GITHUB_TOKEN" -H "Accept: application/vnd.github+json" "https://api.github.com/repos/$OWNER/$REPO/actions/runs/$RUN_ID")
  status=$(echo "$run" | jq -r '.status')
  conclusion=$(echo "$run" | jq -r '.conclusion // empty')
  echo "Status: $status, conclusion: ${conclusion:-none}"
  if [ "$status" = "completed" ]; then break; fi
  sleep 10
done

if [ "$conclusion" != "success" ]; then
  echo "Workflow finished with conclusion: $conclusion" >&2
  exit 4
fi

echo "Workflow succeeded. Fetching artifacts..."
artifacts=$(curl -s -H "Authorization: token $GITHUB_TOKEN" -H "Accept: application/vnd.github+json" "https://api.github.com/repos/$OWNER/$REPO/actions/runs/$RUN_ID/artifacts")
artifact_id=$(echo "$artifacts" | jq -r '.artifacts[] | select(.name=="APKs") | .id // empty')
if [ -z "$artifact_id" ]; then
  artifact_id=$(echo "$artifacts" | jq -r '.artifacts[0].id // empty')
fi
if [ -z "$artifact_id" ]; then
  echo "No artifacts found for run $RUN_ID" >&2
  exit 5
fi

echo "Downloading artifact id: $artifact_id"
curl -L -H "Authorization: token $GITHUB_TOKEN" -H "Accept: application/octet-stream" "https://api.github.com/repos/$OWNER/$REPO/actions/artifacts/$artifact_id/zip" -o artifacts.zip
mkdir -p artifacts
unzip -o artifacts.zip -d artifacts
echo "Artifacts extracted to ./artifacts"
