--
-- Rocket class.
--
-- @filename  LgRocket.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-05
--

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class LgRocket.
local strClassName = "LgRocket"
local LgRocket = YwDeclare(strClassName, YwClass(strClassName, YwMonoBehaviour))

-- Member variables.

-- The explosion object.
LgRocket.m_cExplosion = nil

-- Awake method.
function LgRocket:Awake()
    --print("LgRocket:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgRocket!")
        return
    end

    -- Get explosion prefab.
    self.m_cExplosion = self.m_aParameters[1]
end

-- Start method.
function LgRocket:Start()
    --print("LgRocket:Start")
    GameObject.Destroy(self.gameObject, 2.0)
end

-- OnTriggerEnter2D method.
function LgRocket:OnTriggerEnter2D(cOtherCollider2D)
    --print("LgRocket:OnTriggerEnter2D")

    -- If it hits an enemy...
    if "Enemy" == cOtherCollider2D.tag then
        -- ... find the Enemy script and call the Hurt function.
        cOtherCollider2D.gameObject:GetComponent(Enemy):Hurt()

        -- Call the explosion instantiation.
        self:OnExplode()

        -- Destroy the rocket.
        GameObject.Destroy(self.gameObject)
    -- Otherwise if it hits a bomb crate...
    elseif "BombPickup" == cOtherCollider2D.tag then
        -- ... find the Bomb script and call the Explode function.
        cOtherCollider2D.gameObject:GetComponent(Bomb):Explode()

        -- Destroy the bomb crate.
        GameObject.Destroy(cOtherCollider2D.transform.root.gameObject)

        -- Destroy the rocket.
        GameObject.Destroy(self.gameObject)
    -- Otherwise if the player manages to shoot himself...
    elseif "Player" ~= cOtherCollider2D.gameObject.tag then
        -- Instantiate the explosion and destroy the rocket.
        self:OnExplode()
        GameObject.Destroy(self.gameObject)
    else
        -- 
    end
end

-- On explode event.
function LgRocket:OnExplode()
    --print("LgRocket:OnExplode")

    -- Create a quaternion with a random rotation in the z-axis.
    local cRandomRotation = Quaternion.Euler(0.0, 0.0, math.random() * 360.0)

    -- Instantiate the explosion where the rocket is with the random rotation.
    GameObject.Instantiate(self.m_cExplosion, self.transform.position, cRandomRotation)
end

-- Return this class.
return LgRocket
