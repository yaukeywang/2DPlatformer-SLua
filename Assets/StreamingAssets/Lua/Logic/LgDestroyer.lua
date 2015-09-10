--
-- The destroyer class.
--
-- @filename  LgDestroyer.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-05
--

local YwDeclare = YwDeclare
local YwClass = YwClass

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class LgDestroyer.
local strClassName = "LgDestroyer"
local LgDestroyer = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The c# class object.
LgDestroyer.this = false

-- The transform.
LgDestroyer.transform = false

-- The c# gameObject.
LgDestroyer.gameObject = false

-- Whether or not this gameobject should destroyed after a delay, on Awake.
LgDestroyer.m_bDestroyOnAwake = false

-- The delay for destroying it on Awake.
LgDestroyer.m_fAwakeDestroyDelay = 0.0

-- Find a child game object and delete it.
LgDestroyer.m_bFindChild = false

-- Name the child object in Inspector.
LgDestroyer.m_strNamedChild = ""

-- Awake method.
function LgDestroyer:Awake()
    --print("LgDestroyer:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgDestroyer!")
        return
    end

    -- If the gameobject should be destroyed on awake,
    if self.m_bDestroyOnAwake then
        if self.m_bFindChild then
            GameObject.Destroy(self.transform:Find(self.m_strNamedChild).gameObject)
        else
            -- ... destroy the gameobject after the delay.
            GameObject.Destroy(self.gameObject, self.m_fAwakeDestroyDelay)
        end
    end
end

-- Destroy child game object method.
function LgDestroyer:DestroyChildGameObject()
    --print("LgDestroyer:DestroyChildGameObject")

    -- Destroy this child gameobject, this can be called from an Animation Event.
    local cObj = self.transform:Find(self.m_strNamedChild).gameObject
    if not Slua.IsNull(cObj) then
        GameObject.Destroy(cObj)
    end
end

-- Disable child game object method.
function LgDestroyer:DisableChildGameObject()
    --print("LgDestroyer:DisableChildGameObject")

    -- Destroy this child gameobject, this can be called from an Animation Event.
    local cObj = self.transform:Find(self.m_strNamedChild).gameObject
    if cObj.activeSelf then
        cObj:SetActive(false)
    end
end

function LgDestroyer:DestroyGameObject()
    --print("LgDestroyer:DestroyGameObject")

    -- Destroy this gameobject, this can be called from an Animation Event.
    GameObject.Destroy(self.gameObject)
end

-- Return this class.
return LgDestroyer
