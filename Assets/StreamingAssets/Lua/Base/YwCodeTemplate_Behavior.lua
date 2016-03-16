--
-- $(Class) class.
--
-- @filename  $(Class).lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    $(Author) @(AuthorName)@$(Mail).com
-- @date      2016-xx-xx
--

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class $(Class).
local strClassName = "$(Class)"
local $(Class) = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The c# class object.
$(Class).this = false

-- The transform.
$(Class).transform = false

-- The c# gameObject.
$(Class).gameObject = false

-- Constructor.
function $(Class):ctor()
    --print("$(Class):ctor")
end

-- Destructor.
function $(Class):dtor()
    --print("$(Class):dtor")
end

-- Awake method.
function $(Class):Awake()
    --print("$(Class):Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in $(Class)!")
        return
    end
end

-- Start method.
function $(Class):Start()
    --print("$(Class):Start")
end

-- Update method.
function $(Class):Update()
    --print("$(Class):Update")
end

-- Late update method.
function $(Class):LateUpdate()
    --print("$(Class):LateUpdate")
end

-- Fixed update method.
function $(Class):FixedUpdate()
    --print("$(Class):FixedUpdate")
end

-- On destroy method.
function $(Class):OnDestroy()
    --print("$(Class):OnDestroy")
end

-- Return this class.
return $(Class)
