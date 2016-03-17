--
-- Health pickup class.
--
-- @filename  LgPlayerHealth.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-07
--

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class LgPlayerHealth.
local strClassName = "LgPlayerHealth"
local LgPlayerHealth = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The parent class.
LgPlayerHealth.m_cParent = nil

-- The transform.
LgPlayerHealth.transform = false

-- The c# gameObject.
LgPlayerHealth.gameObject = false

-- Params from unity editor.
LgPlayerHealth.m_fHealth = 100.0
LgPlayerHealth.m_fRepeatDamagePeriod = 2.0
LgPlayerHealth.m_fHurtForce = 10.0
LgPlayerHealth.m_fDamageAmount = 10.0
LgPlayerHealth.m_aOuchClips = nil
LgPlayerHealth.m_cGun = nil

-- Reference to the sprite renderer of the health bar. (SpriteRenderer)
LgPlayerHealth.m_cHealthBar = false

-- The time at which the player was last hit.
LgPlayerHealth.m_fLastHitTime = 0.0

-- The local scale of the health bar initially (with full health). (Vector3)
LgPlayerHealth.m_vHealthScale = false

-- Reference to the PlayerControl script.
LgPlayerHealth.m_cPlayerControl = false

-- Reference to the animator.
LgPlayerHealth.m_cAnim = false

-- Constructor.
function LgPlayerHealth:ctor(cParent)
    --print("LgPlayerHealth:ctor")
    self.m_cParent = cParent
    self.gameObject = cParent.gameObject
    self.transform = cParent.gameObject.transform

    -- Setting up references.
    self.m_cHealthBar = GameObject.Find("HealthBar"):GetComponent(SpriteRenderer)
    self.m_cAnim = self.gameObject:GetComponent(Animator)

    -- Getting the intial scale of the healthbar (whilst the player has full health).
    self.m_vHealthScale = self.m_cHealthBar.transform.localScale

    -- Init audio clip array.
    self.m_aOuchClips = {}
end

-- Destructor.
function LgPlayerHealth:dtor()
    --print("LgPlayerHealth:dtor")

    self.m_cParent = nil
    self.gameObject = nil
    self.transform = nil
    self.m_cPlayerControl = nil
    self.m_cHealthBar = nil
    self.m_cAnim = nil
    self.m_aOuchClips = nil
    self.m_cGun = nil
end

-- OnCollisionEnter2D method.
function LgPlayerHealth:OnCollisionEnter2D(cOtherCollider2D)
    --print("LgPlayerHealth:OnCollisionEnter2D")

    -- If the colliding gameobject is an Enemy...
    if "Enemy" == cOtherCollider2D.gameObject.tag then
        -- ... and if the time exceeds the time of the last hit plus the time between hits...
        if Time.time > self.m_fLastHitTime + self.m_fRepeatDamagePeriod then
            -- ... and if the player still has health...
            if self.m_fHealth > 0.0 then
                -- ... take damage and reset the lastHitTime.
                self:TakeDamage(cOtherCollider2D.transform)
                self.m_fLastHitTime = Time.time
            else
                -- If the player doesn't have health, do some stuff, let him fall into the river to reload the level.
                -- Find all of the colliders on the gameobject and set them all to be triggers.
                local aCols = self.gameObject:GetComponents(Collider2D)
                for i = 1, aCols.Length do
                    aCols[i].isTrigger = true
                end

                -- Move all sprite parts of the player to the front.
                local aSprs = self.gameObject:GetComponentsInChildren(SpriteRenderer)
                for i = 1, aSprs.Length do
                    aSprs[i].sortingLayerName = "UI";
                end

                -- ... disable user Player Control script
                self.m_cPlayerControl.m_bEnabled = false

                -- ... disable the Gun script to stop a dead guy shooting a nonexistant bazooka.
                self.m_cGun.enabled = false

                -- ... Trigger the 'Die' animation state.
                self.m_cAnim:SetTrigger("Die")
            end
        end
    end
end

-- Take damage method.
function LgPlayerHealth:TakeDamage(cEnemyTransform)
    --print("LgPlayerHealth:TakeDamage")

    -- Make sure the player can't jump.
    self.m_cPlayerControl.m_bJump = false

    -- Create a vector that's from the enemy to the player with an upwards boost.
    local vHurtVector = self.transform.position - cEnemyTransform.position + Vector3.up * 5.0

    -- Add a force to the player in the direction of the vector and multiply by the hurtForce.
    self.gameObject:GetComponent(Rigidbody2D):AddForce(vHurtVector * self.m_fHurtForce)

    -- Reduce the player's health by 10.
    self.m_fHealth = self.m_fHealth - self.m_fDamageAmount

    -- Update what the health bar looks like.
    self:UpdateHealthBar()

    -- Play a random clip of the player getting hurt.
    local nIdx = Random.Range(1, #self.m_aOuchClips + 1)
    AudioSource.PlayClipAtPoint(self.m_aOuchClips[nIdx], self.transform.position);
end

-- Update health bar method.
function LgPlayerHealth:UpdateHealthBar()
    --print("LgPlayerHealth:UpdateHealthBar")

    -- Set the health bar's colour to proportion of the way between green and red based on the player's health.
    self.m_cHealthBar.material.color = Color.Lerp(Color.green, Color.red, 1.0 - self.m_fHealth * 0.01)

    -- Set the scale of the health bar to be proportional to the player's health.
    self.m_cHealthBar.transform.localScale = Vector3(self.m_vHealthScale.x * self.m_fHealth * 0.01, 1.0, 1.0)
end

-- On destroy method.
function LgPlayerHealth:OnDestroy()
    --print("LgPlayerHealth:OnDestroy")

    self.m_cParent = nil
    self.gameObject = nil
    self.transform = nil
    self.m_cPlayerControl = nil
    self.m_cHealthBar = nil
    self.m_cAnim = nil
    self.m_aOuchClips = nil
    self.m_cGun = nil
end

-- Return this class.
return LgPlayerHealth
