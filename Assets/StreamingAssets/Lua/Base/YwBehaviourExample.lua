--
-- YwBehaviourExample class.
--
-- @filename  YwBehaviourExample.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-08-23
--

local YwDeclare = YwDeclare
local YwClass = YwClass

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class YwBehaviourExample.
local strClassName = "YwBehaviourExample"
local YwBehaviourExample = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The c# class object.
YwBehaviourExample.this = false

-- The transform.
YwBehaviourExample.transform = false

-- The c# gameObject.
YwBehaviourExample.gameObject = false

-- The name.
YwBehaviourExample.m_strName = ""

-- Awake method.
function YwBehaviourExample:Awake()
    print("YwBehaviourExample:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in YwBehaviourExample!")
        return
    end

    print("My name is: " .. self.m_strName)
end

-- Start method.
function YwBehaviourExample:Start()
    print("YwBehaviourExample:Start")
end

-- Update method.
function YwBehaviourExample:Update()
    --print("YwBehaviourExample:Update")
end

-- On destroy method.
function YwBehaviourExample:OnDestroy()
    print("YwBehaviourExample:OnDestroy")
end

-- Return this class.
return YwBehaviourExample
