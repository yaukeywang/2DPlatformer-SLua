--
-- Gun class.
--
-- @filename  LgGun.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-03
--

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

local Input = Input

-- Register new class LgGun.
local strClassName = "LgGun"
local LgGun = YwDeclare(strClassName, YwClass(strClassName, YwMonoBehaviour))

-- Member variables.

-- Params from unity editor.
LgGun.m_fSpeed = 20.0
LgGun.m_cRocket = nil

-- Private.

-- Reference to the PlayerControl script.(PlayerControl)
LgGun.m_cPlayerCtrl = false

-- Reference to the Animator component.(Animator)
LgGun.m_cAnim = false

-- Reference to the AudioSource component.(AudioSource)
LgGun.m_cAudio = false

-- Awake method.
function LgGun:Awake()
    --print("LgGun:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgGun!")
        return
    end

    -- Setting up the references.
    self.m_cAnim = self.transform.root.gameObject:GetComponent(Animator)
    self.m_cAudio = self.gameObject:GetComponent(AudioSource)

    -- Get data bridge.
    local cDataBridge = self.gameObject:GetComponent(YwLuaMonoDataBridge)
    local aFloatArray = cDataBridge.m_floats
    self.m_fSpeed = aFloatArray[1]
    self.m_cRocket = self.m_aParameters[1]
end

-- Update method.
function LgGun:Update()
    --print("LgGun:Update")

    -- If the fire button is pressed...
    if Input.GetButtonDown("Fire1") then
        -- ... set the animator Shoot trigger parameter and play the audioclip.
        self.m_cAnim:SetTrigger("Shoot")
        self.m_cAudio:Play()

        -- If the player is facing right...
        if self.m_cPlayerCtrl.m_bFacingRight then
            -- ... instantiate the rocket facing right and set it's velocity to the right. 
            local cBulletObj = GameObject.Instantiate(self.m_cRocket, self.transform.position, Quaternion.Euler(Vector3(0.0, 0.0, 0.0)))
            local cBulletInstance = cBulletObj:GetComponent(Rigidbody2D)
            cBulletInstance.velocity = Vector2(self.m_fSpeed, 0.0)
        else
            -- Otherwise instantiate the rocket facing left and set it's velocity to the left.
            local cBulletObj = GameObject.Instantiate(self.m_cRocket, self.transform.position, Quaternion.Euler(Vector3(0.0, 0.0, 180.0)))
            local cBulletInstance = cBulletObj:GetComponent(Rigidbody2D)
            cBulletInstance.velocity = Vector2(-self.m_fSpeed, 0.0)
        end
    end
end

-- Return this class.
return LgGun
