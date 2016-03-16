--
-- Lua register file.
--
-- @filename  YwRegister.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-05-28
--

local str_len = string.len

-- YwRegister a new class.
local strClassName = "YwRegister"
local YwRegister = YwDeclare(strClassName, YwClass(strClassName))

-- The object list.
YwRegister.m_aObjectList = false

-- Constructor.
function YwRegister:ctor()
    --print("YwRegister:ctor")
    self.m_aObjectList = {}
end

-- Destructor.
function YwRegister:dtor()
    --print("YwRegister:dtor")
    self.m_aObjectList = nil
end

-- Virtual.
-- Validate this object.
function YwRegister:Validate()
    --print("YwRegister:Validate")
end

-- Virtual.
-- Invalidate this object.
function YwRegister:Invalidate()
    --print("YwRegister:Invalidate")
end

-- Virtual.
function YwRegister:Update()
    --print("YwRegister:Update")
    local aObjList = self.m_aObjectList
    for _, v in pairs(aObjList) do
        if v then
            v:Update()
        end
    end
end

-- YwRegister an object.
function YwRegister:RegisterObject(strName, cYwRegisterObj)
    --print("YwRegister:RegisterObject")
    
    if (0 == str_len(strName)) or (not cYwRegisterObj) then
        return false
    end

    local aObjList = self.m_aObjectList
    if aObjList[strName] then
        return false
    end

    aObjList[strName] = cYwRegisterObj
    return true
end

-- UnYwregister an object.
function YwRegister:UnregisterObject(strName)
    --print("YwRegister:UnregisterObject")

    if 0 == str_len(strName) then
        return false
    end

    local aObjList = self.m_aObjectList
    if not aObjList[strName] then
        return false
    end

    aObjList[strName] = nil
    return true
end

-- Get an Ywregister object by name.
function YwRegister:GetRegisterObject(strName)
    --print("YwRegister:GetRegisterObject")

    if 0 == str_len(strName) then
        return false
    end

    return self.m_aObjectList[strName]
end

-- Return this class.
return YwRegister
