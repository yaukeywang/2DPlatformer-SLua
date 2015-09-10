--
-- Lua register file.
--
-- @filename  YwRegister.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-05-28
--

local YwDeclare = require "Base/YwGlobal"

local YwClass = YwClass
local str_len = string.len

-- YwRegister a new class.
local strClassName = "YwRegister"
local YwRegister = YwDeclare(strClassName, YwClass(strClassName))

-- The object list.
YwRegister.m_cObjectList = false

-- Constructor.
function YwRegister:Constructor()
    --print("YwRegister:Constructor")
    self.m_cObjectList = {}
end

-- Destructor.
function YwRegister:Destructor()
    --print("YwRegister:Destructor")
end

-- Virtual.
-- Validate this object.
function YwRegister:Validate()
    -- body
end

-- Virtual.
-- Invalidate this object.
function YwRegister:Invalidate()
    -- body
end

-- Virtual.
function YwRegister:Update()
    local ObjList = self.m_cObjectList
    for _, v in pairs(ObjList) do
        if v then
            v:Update()
        end
    end
end

-- YwRegister an object.
function YwRegister:YwRegisterObject(Name, YwRegisterObj)
    --print("YwRegister:YwRegisterObject")
    
    if 0 == str_len(Name) then
        return false
    end

    local ObjList = self.m_cObjectList
    if ObjList[Name] then
        return false
    end

    ObjList[Name] = YwRegisterObj
    return true
end

-- UnYwregister an object.
function YwRegister:UnYwregisterObject(Name)
    if 0 == str_len(Name) then
        return false
    end

    local ObjList = self.m_cObjectList
    if not ObjList[Name] then
        return false
    end

    ObjList[Name] = nil
    return true
end

-- Get an Ywregister object by name.
function YwRegister:GetYwRegisterObject(Name)
     if 0 == str_len(Name) then
        return false
    end

    return self.m_cObjectList[Name]
end

-- Return this class.
return YwRegister
