using UnityEngine;

/// <summary>
/// Global game constants and configuration values.
/// Centralized configuration for easy tweaking and balancing.
/// </summary>
public static class Constants
{
    // ==================== PLAYER ====================
    public static class Player
    {
        public const float MOVE_SPEED = 5f;
        public const float ROTATION_SPEED = 10f;
        public const float BASE_HP = 100f;
        public const float BASE_MANA = 50f;
        public const float BASE_DAMAGE = 10f;
        public const float ATTACK_COOLDOWN = 0.8f;
        public const float ATTACK_RANGE = 2f;
        public const float ATTACK_ANGLE = 90f; // Cone angle for melee attacks
        public const float DASH_SPEED = 15f;
        public const float DASH_DURATION = 0.3f;
        public const float DASH_COOLDOWN = 2f;
        public const float CRIT_CHANCE = 0.15f; // 15%
        public const float CRIT_MULTIPLIER = 1.5f;
    }

    // ==================== ENEMY ====================
    public static class Enemy
    {
        public const float BASE_MOVE_SPEED = 3f;
        public const float BASE_HP = 30f;
        public const float BASE_DAMAGE = 5f;
        public const float ATTACK_COOLDOWN = 1.2f;
        public const float ATTACK_RANGE = 1.5f;
        public const float AGGRO_RANGE = 10f;
        public const float PATROL_RANGE = 5f;
        public const float STOP_DISTANCE = 0.5f;
        
        // Elite modifiers
        public const float ELITE_HP_MULTIPLIER = 1.5f;
        public const float ELITE_DAMAGE_MULTIPLIER = 1.25f;
    }

    // ==================== COMBAT ====================
    public static class Combat
    {
        public const float DAMAGE_NUMBER_LIFETIME = 1.5f;
        public const float DAMAGE_NUMBER_RISE_SPEED = 2f;
        public const float HIT_FLASH_DURATION = 0.1f;
        public const float KNOCKBACK_FORCE = 5f;
        public const float KNOCKBACK_DURATION = 0.2f;
    }

    // ==================== LOOT ====================
    public static class Loot
    {
        public const float LOOT_DESPAWN_TIME = 60f;
        public const float LOOT_PICKUP_RANGE = 2f;
        public const float LOOT_FOUNTAIN_FORCE = 10f;
        public const float LOOT_FOUNTAIN_ANGLE = 45f;
        
        // Drop rates
        public const float COMMON_DROP_RATE = 0.6f;
        public const float MAGIC_DROP_RATE = 0.25f;
        public const float RARE_DROP_RATE = 0.12f;
        public const float LEGENDARY_DROP_RATE = 0.03f;
    }

    // ==================== CAMERA ====================
    public static class Camera
    {
        public const float ISOMETRIC_ANGLE = 45f; // Isometric view angle
        public const float CAMERA_DISTANCE = 10f;
        public const float CAMERA_HEIGHT = 8f;
        public const float FOLLOW_SPEED = 5f;
        public const float ZOOM_SPEED = 2f;
        public const float MIN_ZOOM = 5f;
        public const float MAX_ZOOM = 15f;
    }

    // ==================== UI ====================
    public static class UI
    {
        public const float HEALTH_BAR_WIDTH = 2f;
        public const float HEALTH_BAR_HEIGHT = 0.3f;
        public const float HEALTH_BAR_OFFSET_Y = 1.5f;
    }

    // ==================== EXPERIENCE & LEVELING ====================
    public static class Experience
    {
        public const float BASE_EXP_REWARD = 10f;
        public const float EXP_MULTIPLIER_PER_LEVEL = 1.1f;
        public const int BASE_EXP_TO_LEVEL = 100;
        public const int MAX_LEVEL = 100;
    }

    // ==================== MONETIZATION ====================
    public static class Monetization
    {
        public const float GAME_PASS_DURATION_DAYS = 30f;
        public const float XP_BOOST_MULTIPLIER = 1.5f;
        public const float PREMIUM_CURRENCY_MULTIPLIER = 1.2f;
        
        // Premium chest rewards
        public const int COMMON_CHEST_GOLD = 500;
        public const int RARE_CHEST_GOLD = 1500;
        public const int LEGENDARY_CHEST_GOLD = 5000;
    }

    // ==================== ANIMATION ====================
    public static class Animation
    {
        public const string PARAM_MOVE_SPEED = "MoveSpeed";
        public const string PARAM_IS_ATTACKING = "IsAttacking";
        public const string PARAM_IS_DEAD = "IsDead";
        public const string PARAM_HIT = "Hit";
    }

    // ==================== LAYERS ====================
    public static class Layers
    {
        public const string PLAYER = "Player";
        public const string ENEMY = "Enemy";
        public const string LOOT = "Loot";
        public const string GROUND = "Ground";
    }

    // ==================== TAGS ====================
    public static class Tags
    {
        public const string PLAYER = "Player";
        public const string ENEMY = "Enemy";
        public const string LOOT = "Loot";
    }
}
