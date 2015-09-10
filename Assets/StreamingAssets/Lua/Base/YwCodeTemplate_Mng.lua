--
-- $(Class) class.
--
-- @filename  $(Class).lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    $(Author) $(AuthorName)@$(Mail).com
-- @date      2015-xx-xx
--

local YwRegisterObject = require "Base/YwRegisterObject"

local YwDeclare = YwDeclare
local YwClass = YwClass

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class $(Class).
local strClassName = "$(Class)"
local $(Class) = YwDeclare(strClassName, YwClass(strClassName, YwRegisterObject))

-- Member variables.

-- The global instance holder.
M.m_cInstance = false

-- Constructor.
function $(Class):Constructor(cRegister)
    --print("$(Class):Constructor")

    if M.m_cInstance then
        DLogError("You have already create a $(Class) instance!")
        return
    end

    -- Set the global instance.
    M.m_cInstance = self
end

-- Destructor.
function $(Class):Destructor()
    --print("$(Class):Destructor")

    -- Release the global instance.
    M.m_cInstance = nil
end

-- Static function.
-- Get the instance.
function $(Class).Instance()
    --print("$(Class):Instance")

    -- Check
    if not $(Class).m_cInstance then
        $(Class).new(Dispatcher.Instance())
    end

    return $(Class).m_cInstance
end

-- Virtual.
-- Init.
function $(Class):Init()
    --print("$(Class):Init")
end

-- Virtual.
-- Release.
function $(Class):Release()
    --print("$(Class):Release")
end

-- Virtual.
-- Update.
function $(Class):Update()
    --print("$(Class):Update")

    -- Super class.
    $(Class).super.Update(self)
end

-- Return this class.
return $(Class)
