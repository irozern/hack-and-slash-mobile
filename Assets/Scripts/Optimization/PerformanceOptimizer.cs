using UnityEngine;
using System.Collections.Generic;
using System;

namespace HackSlash.Optimization
{
    /// <summary>
    /// Monitors and optimizes game performance.
    /// Tracks FPS, memory usage, and provides optimization recommendations.
    /// </summary>
    public class PerformanceOptimizer : MonoBehaviour
    {
        public static PerformanceOptimizer Instance { get; private set; }

        [System.Serializable]
        public class PerformanceMetrics
        {
            public float fps;
            public float frameTime;
            public float memoryUsage;
            public float gpuMemory;
            public int drawCalls;
            public int triangles;
            public int vertices;
            public DateTime timestamp;
        }

        [System.Serializable]
        public class OptimizationSettings
        {
            public bool enableObjectPooling = true;
            public bool enableLOD = true;
            public bool enableOcclusion = true;
            public bool enableDynamicResolution = false;
            public int targetFPS = 60;
            public int maxDrawCalls = 500;
            public int maxParticles = 1000;
        }

        private PerformanceMetrics currentMetrics;
        private Queue<PerformanceMetrics> metricsHistory = new();
        private OptimizationSettings settings = new();
        private float fpsUpdateTimer = 0f;
        private int frameCount = 0;
        private float deltaTime = 0f;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeOptimizer();
        }

        void Update()
        {
            UpdateMetrics();
            CheckPerformance();
        }

        /// <summary>
        /// Initialize performance optimizer.
        /// </summary>
        private void InitializeOptimizer()
        {
            currentMetrics = new PerformanceMetrics();
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = settings.targetFPS;
            Debug.Log("Performance optimizer initialized");
        }

        /// <summary>
        /// Update performance metrics.
        /// </summary>
        private void UpdateMetrics()
        {
            frameCount++;
            deltaTime += Time.deltaTime;
            fpsUpdateTimer += Time.deltaTime;

            if (fpsUpdateTimer >= 1f)
            {
                currentMetrics.fps = frameCount / fpsUpdateTimer;
                currentMetrics.frameTime = (fpsUpdateTimer / frameCount) * 1000f;
                currentMetrics.memoryUsage = SystemInfo.systemMemorySize / 1024f;
                currentMetrics.gpuMemory = SystemInfo.graphicsMemorySize / 1024f;
                currentMetrics.timestamp = DateTime.Now;

                metricsHistory.Enqueue(currentMetrics);

                // Keep only last 60 frames
                if (metricsHistory.Count > 60)
                    metricsHistory.Dequeue();

                frameCount = 0;
                fpsUpdateTimer = 0f;

                OnMetricsUpdated?.Invoke(currentMetrics);
            }
        }

        /// <summary>
        /// Check performance and apply optimizations.
        /// </summary>
        private void CheckPerformance()
        {
            if (currentMetrics.fps < settings.targetFPS * 0.8f)
            {
                // Performance is degrading
                ApplyPerformanceOptimizations();
                OnPerformanceDegraded?.Invoke(currentMetrics.fps);
            }
            else if (currentMetrics.fps > settings.targetFPS * 1.1f)
            {
                // Performance is good, can improve quality
                ApplyQualityImprovements();
                OnPerformanceImproved?.Invoke(currentMetrics.fps);
            }
        }

        /// <summary>
        /// Apply performance optimizations.
        /// </summary>
        private void ApplyPerformanceOptimizations()
        {
            // Reduce quality settings
            if (QualitySettings.GetQualityLevel() > 0)
            {
                QualitySettings.DecreaseLevel();
                Debug.Log($"Quality decreased to level {QualitySettings.GetQualityLevel()}");
            }

            // Reduce particle count
            ParticleSystem[] particles = FindObjectsOfType<ParticleSystem>();
            foreach (ParticleSystem ps in particles)
            {
                var main = ps.main;
                main.maxParticles = Mathf.Max(100, main.maxParticles / 2);
            }

            // Enable LOD
            if (settings.enableLOD)
            {
                LODGroup[] lodGroups = FindObjectsOfType<LODGroup>();
                foreach (LODGroup lod in lodGroups)
                {
                    lod.enabled = true;
                }
            }
        }

        /// <summary>
        /// Apply quality improvements.
        /// </summary>
        private void ApplyQualityImprovements()
        {
            // Increase quality settings
            if (QualitySettings.GetQualityLevel() < QualitySettings.names.Length - 1)
            {
                QualitySettings.IncreaseLevel();
                Debug.Log($"Quality increased to level {QualitySettings.GetQualityLevel()}");
            }

            // Increase particle count
            ParticleSystem[] particles = FindObjectsOfType<ParticleSystem>();
            foreach (ParticleSystem ps in particles)
            {
                var main = ps.main;
                main.maxParticles = Mathf.Min(settings.maxParticles, main.maxParticles * 2);
            }
        }

        /// <summary>
        /// Get current metrics.
        /// </summary>
        public PerformanceMetrics GetCurrentMetrics()
        {
            return currentMetrics;
        }

        /// <summary>
        /// Get average metrics.
        /// </summary>
        public PerformanceMetrics GetAverageMetrics()
        {
            if (metricsHistory.Count == 0)
                return currentMetrics;

            PerformanceMetrics average = new PerformanceMetrics();
            float totalFps = 0f;
            float totalFrameTime = 0f;

            foreach (var metrics in metricsHistory)
            {
                totalFps += metrics.fps;
                totalFrameTime += metrics.frameTime;
            }

            average.fps = totalFps / metricsHistory.Count;
            average.frameTime = totalFrameTime / metricsHistory.Count;

            return average;
        }

        /// <summary>
        /// Get performance report.
        /// </summary>
        public Dictionary<string, object> GetPerformanceReport()
        {
            PerformanceMetrics avg = GetAverageMetrics();

            Dictionary<string, object> report = new();
            report["CurrentFPS"] = currentMetrics.fps;
            report["AverageFPS"] = avg.fps;
            report["CurrentFrameTime"] = currentMetrics.frameTime;
            report["AverageFrameTime"] = avg.frameTime;
            report["MemoryUsage"] = currentMetrics.memoryUsage;
            report["GPUMemory"] = currentMetrics.gpuMemory;
            report["QualityLevel"] = QualitySettings.GetQualityLevel();
            report["TargetFPS"] = settings.targetFPS;
            report["IsOptimized"] = currentMetrics.fps >= settings.targetFPS * 0.95f;

            return report;
        }

        /// <summary>
        /// Enable object pooling.
        /// </summary>
        public void EnableObjectPooling(bool enable)
        {
            settings.enableObjectPooling = enable;
            Debug.Log($"Object pooling: {(enable ? "enabled" : "disabled")}");
        }

        /// <summary>
        /// Enable LOD.
        /// </summary>
        public void EnableLOD(bool enable)
        {
            settings.enableLOD = enable;
            LODGroup[] lodGroups = FindObjectsOfType<LODGroup>();
            foreach (LODGroup lod in lodGroups)
            {
                lod.enabled = enable;
            }
            Debug.Log($"LOD: {(enable ? "enabled" : "disabled")}");
        }

        /// <summary>
        /// Enable dynamic resolution.
        /// </summary>
        public void EnableDynamicResolution(bool enable)
        {
            settings.enableDynamicResolution = enable;
            if (enable)
            {
                ScalableBufferManager.ResizeBuffers(0.8f, 0.8f);
            }
            else
            {
                ScalableBufferManager.ResizeBuffers(1f, 1f);
            }
            Debug.Log($"Dynamic resolution: {(enable ? "enabled" : "disabled")}");
        }

        /// <summary>
        /// Set target FPS.
        /// </summary>
        public void SetTargetFPS(int fps)
        {
            settings.targetFPS = fps;
            Application.targetFrameRate = fps;
            Debug.Log($"Target FPS set to {fps}");
        }

        /// <summary>
        /// Get optimization settings.
        /// </summary>
        public OptimizationSettings GetOptimizationSettings()
        {
            return settings;
        }

        // Events
        public event Action<PerformanceMetrics> OnMetricsUpdated;
        public event Action<float> OnPerformanceDegraded;
        public event Action<float> OnPerformanceImproved;
    }
}
