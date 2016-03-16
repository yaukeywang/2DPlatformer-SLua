--
-- Lua register file.
--
-- @filename  YwRegisterObject.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-05-28
--

local str_len = string.len

-- Register a new class.
local strClassName = "YwRegisterObject"
local YwRegisterObject = YwDeclare(strClassName, YwClass(strClassName))

-- Hold on the register.
YwRegisterObject.m_cRegister = false

-- Constructor.
function YwRegisterObject:Constructor(cRegister)
    --print("YwRegisterObject:Constructor")

    if not cRegister then
        return
    end

    self.m_cRegister = cRegister
    self.m_cRegister:RegisterObject(self:ToString(), self)
end

-- Destructor.
function YwRegisterObject:Destructor()
    --print("YwRegisterObject:Destructor")

    local cRegister = self.m_cRegister
    if cRegister then
        cRegister:UnregisterObject(self:ToString())
        cRegister = nil
    end
end

-- Get the register.
function YwRegisterObject:GetRegister()
    return self.m_cRegister
end

-- Virtual.
-- Validate this object.
function YwRegisterObject:Validate()
    -- body
end

-- Virtual.
-- Invalidate this object.
function YwRegisterObject:Invalidate()
    -- body
end

-- Virtual.
function YwRegisterObject:Update()
    -- Body
end

-- Return this class.
return YwRegisterObject
