--
-- The base mono behaviour class at lua side.
--
-- @filename  YwMonoBehaviour.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey yaukeywang@gmail.com
-- @date      2015-11-28
--

-- Register new class YwMonoBehaviour.
local strClassName = "YwMonoBehaviour"
local YwMonoBehaviour = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The C# mono behaviour.
YwMonoBehaviour.this = nil

-- The transform of the C# game object.
YwMonoBehaviour.transform = nil

-- The game object of the C# mono behaviour.
YwMonoBehaviour.gameObject = nil

-- All the parameters passed by C#.
YwMonoBehaviour.m_aParameters = nil

-- Use update or not.
YwMonoBehaviour.m_bUpdate = false

-- Use late update or not.
YwMonoBehaviour.m_bLateUpdate = false

-- Use fixed update or not.
YwMonoBehaviour.m_bFixedUpdate = false

-- Use lite update or not.
YwMonoBehaviour.m_bLiteUpdate = false

-- Constructor.
function YwMonoBehaviour:Constructor()
    --print("YwMonoBehaviour:Constructor")
    self.m_aParameters = {}
end

-- Destructor.
function YwMonoBehaviour:Destructor()
    --print("YwMonoBehaviour:Destructor")
    self.this = nil
    self.transform = nil
    self.gameObject = nil
    self.m_aParameters = nil
end

-- The base of the awake method.
function YwMonoBehaviour:AwakeBase()
	--print("YwMonoBehaviour:AwakeBase")

	-- Check base variable.
	if (not self.this) or (not self.transform) or (not self.gameObject) then
        YwDebug.LogError("Init error in YwMonoBehaviour!")
        return
    end

	-- Check if need to register update.
	if self.m_bUpdate or self.m_bLateUpdate or self.m_bFixedUpdate then
		YwMonoBehaviourUpdater.Instance():RegisterUpdate(self:ToString(), self, self.m_bUpdate, self.m_bLateUpdate, self.m_bFixedUpdate, self.m_bLiteUpdate)
	end
end

-- The base of the on enable method.
function YwMonoBehaviour:OnEnableBase()
	--print("YwMonoBehaviour:OnEnableBase")
	if self.m_bUpdate or self.m_bLateUpdate or self.m_bFixedUpdate then
		YwMonoBehaviourUpdater.Instance():SetActive(self:ToString(), true)
	end
end

-- The base of the on disable method.
function YwMonoBehaviour:OnDisableBase()
	--print("YwMonoBehaviour:OnDisableBase")
	if self.m_bUpdate or self.m_bLateUpdate or self.m_bFixedUpdate then
		YwMonoBehaviourUpdater.Instance():SetActive(self:ToString(), false)
	end
end

-- The base of the on destroy method.
function YwMonoBehaviour:OnDestroyBase()
	--print("YwMonoBehaviour:OnDestroyBase")
	if self.m_bUpdate or self.m_bLateUpdate or self.m_bFixedUpdate then
		YwMonoBehaviourUpdater.Instance():UnregisterUpdate(self:ToString())
	end
end

-- Return this class.
return YwMonoBehaviour
