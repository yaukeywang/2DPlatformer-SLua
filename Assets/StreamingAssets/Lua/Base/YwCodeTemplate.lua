--
-- $(Class) class.
--
-- @filename  $(Class).lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey yaukeywang@gmail.com
-- @date      2016-xx-xx
--

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class $(Class).
local strClassName = "$(Class)"
local $(Class) = YwDeclare(strClassName, YwClass(strClassName, $(SuperClass)))

-- Member variables.

-- Constructor.
function $(Class):ctor()
    --print("$(Class):ctor")
end

-- Destructor.
function $(Class):dtor()
    --print("$(Class):dtor")
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

-- Static.
-- The static method.
function $(Class).StaticMethod()
    --print("$(Class):StaticMethod")
end

-- Return this class.
return $(Class)
