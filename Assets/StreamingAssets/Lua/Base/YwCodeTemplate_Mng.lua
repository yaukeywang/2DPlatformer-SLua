--
-- $(Class) class.
--
-- @filename  $(Class).lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey yaukeywang@gmail.com
-- @date      2016-xx-xx
--

local YwRegisterObject = require "Base/YwRegisterObject"

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class $(Class).
local strClassName = "$(Class)"
local $(Class) = YwDeclare(strClassName, YwClass(strClassName, YwRegisterObject))

-- Member variables.

-- The global instance holder.
$(Class).m_cInstance = false

-- Constructor.
function $(Class):ctor(cRegister)
    --print("$(Class):ctor")

    if $(Class).m_cInstance then
        DLogError("You have already create a $(Class) instance!")
        return
    end

    -- Set the global instance.
    $(Class).m_cInstance = self
end

-- Destructor.
function $(Class):dtor()
    --print("$(Class):dtor")

    -- Release the global instance.
    $(Class).m_cInstance = nil
end

-- Static function.
-- Get the instance.
function $(Class).Instance()
    --print("$(Class):Instance")

    -- Check
    if not $(Class).m_cInstance then
        $(Class).new(YwDispatcher.Instance())
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
