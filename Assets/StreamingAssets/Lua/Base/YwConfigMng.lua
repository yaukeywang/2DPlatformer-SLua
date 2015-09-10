--
-- Lua configure class.
--
-- @filename  YwConfigMng.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-07-06
--

local YwRegisterObject = require "Base/YwRegisterObject"

local YwDeclare = YwDeclare
local YwClass = YwClass
local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

--
-- The game param.
local strGameParamName = "SGameParam"
local SGameParam = YwDeclare(strGameParamName, YwClass(strGameParamName))

-- Member variables.

-- Loaded or not.
SGameParam.m_bIsLoad = false

-- Task max parallel count.
SGameParam.m_nTaskMaxParallelCount = 20

function SGameParam:Constructor()
    -- body
end

function SGameParam:Destructor()
    -- body
end

-- 
-- Formula param.
local strFormulaParamName = "SFormulaParam"
local SFormulaParam = YwDeclare(strFormulaParamName, YwClass(strFormulaParamName))

-- Member variables.

SFormulaParam.m_bIsLoad = false
SFormulaParam.m_fStrengthToAtk = 0.6

function SFormulaParam:Constructor()
    -- body
end

function SFormulaParam:Destructor()
    -- body
end

--
-- Test param.
local strTestParamName = "STestParam"
local STestParam = YwDeclare(strTestParamName, YwClass(strTestParamName))

-- Member variables.

STestParam.m_bIsLoad = false

function STestParam:Constructor()
    -- body
end

function STestParam:Destructor()
    -- body
end

--
-- Register a new class.
local strClassName = "YwConfigMng"
local YwConfigMng = YwDeclare(strClassName, YwClass(strClassName, YwRegisterObject))

-- Hold the super class update method.
local SuperUpdate = YwConfigMng.super.Update

-- Member variables.

-- The game paraConfigMng.
YwConfigMng.m_cGameParam = false

-- The formula paraConfigMng.
YwConfigMng.m_cFormulaParam = false

-- The test paraConfigMng.
YwConfigMng.m_cTestParam = false

-- The global instance holder.
YwConfigMng.m_cInstance = false

-- Constructor.
function YwConfigMng:Constructor(cRegister)
    --print("YwConfigMng:Constructor")

    if YwConfigMng.m_cInstance then
        DLogError("You have already create a YwConfigMng instance!")
        return
    end

    -- Alloc params.
    self.m_cGameParam = SGameParam()
    self.m_cFormulaParam = SFormulaParam()
    self.m_cTestParam = STestParam()

    -- Set the global instance.
    YwConfigMng.m_cInstance = self
end

-- Destructor.
function YwConfigMng:Destructor()
    --print("YwConfigMng:Destructor")

    -- Clear params.
    self.m_cGameParam = nil
    self.m_cFormulaParam = nil
    self.m_cTestParam = nil

    -- Clear instance.
    YwConfigMng.m_cInstance = nil
end

-- Static function.
-- Get the instance.
function YwConfigMng.Instance()
    --print("YwConfigMng:Instance")

    -- Check
    if not YwConfigMng.m_cInstance then
        YwConfigMng.new(Dispatcher.Instance())
    end

    return YwConfigMng.m_cInstance
end

-- Virtual.
-- function YwConfigMng:Update()
--     -- Base update first.
--     SuperUpdate(self)
-- end

-- Return this class.
return YwConfigMng
