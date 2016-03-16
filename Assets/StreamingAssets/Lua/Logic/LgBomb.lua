--
-- Bomb class.
--
-- @filename  LgBomb.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-01
--

local YwDeclare = YwDeclare
local YwClass = YwClass

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class LgBomb.
local strClassName = "LgBomb"
local LgBomb = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The c# class object.
LgBomb.this = false

-- The transform.
LgBomb.transform = false

-- The c# gameObject.
LgBomb.gameObject = false

-- Public.

-- Prefab of explosion effect.
LgBomb.m_cExplosion = false

-- Private.
-- Reference to the player's LayBombs script.
LgBomb.m_cLayBombs = false

-- Reference to the PickupSpawner script.
LgBomb.m_cPickupSpawner = false

-- Reference to the particle system of the explosion effect.
LgBomb.m_cExplosionFx = false

-- Awake method.
function LgBomb:Awake()
    --print("LgBomb:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgBomb!")
        return
    end

    self.m_cExplosionFx = GameObject.FindGameObjectWithTag("ExplosionFX"):GetComponent(ParticleSystem)
    self.m_cPickupSpawner = GameObject.Find("pickupManager"):GetComponent(PickupSpawner)
    if GameObject.FindGameObjectWithTag("Player") then
        self.m_cLayBombs = GameObject.FindGameObjectWithTag("Player"):GetComponent(LayBombs)
    end
end

-- Start method.
function LgBomb:Start()
    --print("LgBomb:Start")

    --If the bomb has no parent, it has been laid by the player and should detonate.
    if self.transform.root == self.transform then
        self:BombDetonation()
    end
end

-- Explode.
function LgBomb:Explode()
    --print("LgBomb:Explode")

    local this = self.this

    -- The player is now free to lay bombs when he has them.
    self.m_cLayBombs.BombLaid = false

    -- Make the pickup spawner start to deliver a new pickup.
    self.m_cPickupSpawner:DeliverPickup()

    -- Find all the colliders on the Enemies layer within the bombRadius.
    local aEnemies = Physics2D.OverlapCircleAll(self.transform.position, this.m_bombRadius, 1 << LayerMask.NameToLayer("Enemies"))

    -- For each collider...
    for i = 1, aEnemies.Length do
        -- Check if it has a rigidbody (since there is only one per enemy, on the parent).
        local cRb = aEnemies[i]:GetComponent(Rigidbody2D)
        if cRb and ("Enemy" == cRb.tag) then
            -- Find the Enemy script and set the enemy's health to zero.
            cRb.gameObject:GetComponent(Enemy).m_HP = 0

            -- Find a vector from the bomb to the enemy.
            local vDeltaPos = cRb.transform.position - self.transform.position

            -- Apply a force in this direction with a magnitude of bombForce.
            local vForce = vDeltaPos.normalized * this.m_bombForce
            cRb:AddForce(vForce)
        end
    end

    -- Set the explosion effect's position to the bomb's position and play the particle system.
    self.m_cExplosionFx.transform.position = self.transform.position
    self.m_cExplosionFx:Play()

    -- Instantiate the explosion prefab.
    GameObject.Instantiate(this.m_explosion, self.transform.position, Quaternion.identity)

    -- Play the explosion sound effect.
    AudioSource.PlayClipAtPoint(this.m_boom, self.transform.position)

    -- Destroy the bomb.
    GameObject.Destroy(self.gameObject)
end

-- LgBomb detonation.
function LgBomb:BombDetonation()
    --print("LgBomb:BombDetonation")

    -- Check the validation.
    if Slua.IsNull(self.gameObject) then
        return
    end

    -- LgBomb detonation.
    local cCo = coroutine.create(function ()
        -- Play the fuse seconds.
        AudioSource.PlayClipAtPoint(self.this.m_fuse, self.transform.position)

        -- Wait for 2 seconds.
        Yield(WaitForSeconds(self.this.m_fuseTime))

        -- Check the validation.
        if Slua.IsNull(self.gameObject) then
            return
        end

        -- Explode the bomb.
        self:Explode()
    end)

    coroutine.resume(cCo)
end

-- Return this class.
return LgBomb
