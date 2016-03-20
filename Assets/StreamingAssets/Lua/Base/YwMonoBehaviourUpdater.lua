--
-- The mono behaviour updater class.
--
-- @filename  YwMonoBehaviourUpdater.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey yaukeywang@gmail.com
-- @date      2015-12-02
--

-- Global to local.
local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError
local str_len = string.len

-------------------------------------------------------
-- Register new class YwUpdateNode for updater.
local YwUpdateNode = YwClass("YwUpdateNode")

-- The name of the register.
YwUpdateNode.m_strName = ""

-- The register object.
YwUpdateNode.m_cObject = nil

-- The update flag.
YwUpdateNode.m_bToggleUpdate = false

-- The late update flag.
YwUpdateNode.m_bToggleLateUpdate = false

-- The fixed update flag.
YwUpdateNode.m_bToggleFixedUpdate = false

-- The lite update flag.
YwUpdateNode.m_bToggleLiteUpdate = false

-- The active flag.
YwUpdateNode.m_bActive = false

-- Constructor.
function YwUpdateNode:ctor(strName, cObject, bUpdate, bLateUpdate, bFixedUpdate, bLiteUpdate)
	--print("YwUpdateNode:ctor")
    self.m_strName = strName
	self.m_cObject = cObject
	self.m_bToggleUpdate = bUpdate
	self.m_bToggleLateUpdate = bLateUpdate
	self.m_bToggleFixedUpdate = bFixedUpdate
	self.m_bToggleLiteUpdate = bLiteUpdate
	self.m_bActive = true
end

-- Destructor.
function YwUpdateNode:dtor()
	--print("YwUpdateNode:dtor")
    self.m_strName = ""
	self.m_cObject = nil
	self.m_bToggleUpdate = false
	self.m_bToggleLateUpdate = false
	self.m_bToggleFixedUpdate = false
	self.m_bToggleLiteUpdate = false
	self.m_bActive = false
end

-------------------------------------------------------
-- Register new class YwMonoBehaviourUpdater.
local strClassName = "YwMonoBehaviourUpdater"
local YwMonoBehaviourUpdater = YwDeclare(strClassName, YwClass(strClassName, YwRegister))

-- Member variables.

-- The udpate node store in order by index.
YwMonoBehaviourUpdater.m_aObjectListInOrder = nil

-- The global instance holder.
YwMonoBehaviourUpdater.m_cInstance = nil

-- Constructor.
function YwMonoBehaviourUpdater:ctor()
    --print("YwMonoBehaviourUpdater:ctor")

    if YwMonoBehaviourUpdater.m_cInstance then
        DLogError("You have already create a YwMonoBehaviourUpdater instance!")
        return
    end

    -- Assign the instance.
    YwMonoBehaviourUpdater.m_cInstance = self

    -- Init the object list in order table.
    self.m_aObjectListInOrder = {}
end

-- Destructor.
function YwMonoBehaviourUpdater:dtor()
    --print("YwMonoBehaviourUpdater:dtor")
    self.m_aObjectListInOrder = nil
end

-- Update method.
function YwMonoBehaviourUpdater:Update()
    --print("YwMonoBehaviourUpdater:Update")

    local cUpdateNode = nil
    local aObjectArray = self.m_aObjectListInOrder
    for i = 1, #aObjectArray do
    	cUpdateNode = aObjectArray[i]
    	if cUpdateNode.m_bActive and cUpdateNode.m_bToggleUpdate and cUpdateNode.m_cObject.Update then
    		cUpdateNode.m_cObject:Update()
    	end
    end
end

-- Late update method.
function YwMonoBehaviourUpdater:LateUpdate()
	--print("YwMonoBehaviourUpdater:LateUpdate")

	local cUpdateNode = nil
    local aObjectArray = self.m_aObjectListInOrder
    for i = 1, #aObjectArray do
    	cUpdateNode = aObjectArray[i]
    	if cUpdateNode.m_bActive and cUpdateNode.m_bToggleLateUpdate and cUpdateNode.m_cObject.LateUpdate then
    		cUpdateNode.m_cObject:LateUpdate()
    	end
    end
end

-- Fixed update method.
function YwMonoBehaviourUpdater:FixedUpdate()
	--print("YwMonoBehaviourUpdater:FixedUpdate")

	local cUpdateNode = nil
    local aObjectArray = self.m_aObjectListInOrder
    for i = 1, #aObjectArray do
    	cUpdateNode = aObjectArray[i]
    	if cUpdateNode.m_bActive and cUpdateNode.m_bToggleFixedUpdate and cUpdateNode.m_cObject.FixedUpdate then
    		cUpdateNode.m_cObject:FixedUpdate()
    	end
    end
end

-- Lite update method.
function YwMonoBehaviourUpdater:LiteUpdate()
	--print("YwMonoBehaviourUpdater:LiteUpdate")

	local cUpdateNode = nil
    local aObjectArray = self.m_aObjectListInOrder
    for i = 1, #aObjectArray do
    	cUpdateNode = aObjectArray[i]
    	if cUpdateNode.m_bActive and cUpdateNode.m_bToggleLiteUpdate and cUpdateNode.m_cObject.LiteUpdate then
    		cUpdateNode.m_cObject:LiteUpdate()
    	end
    end
end

-- Register a new object for updater by name.
function YwMonoBehaviourUpdater:RegisterUpdate(strName, cObject, bUpdate, bLateUpdate, bFixedUpdate, bLiteUpdate)
	--print("YwMonoBehaviourUpdater:RegisterUpdate", strName)

	if (0 == str_len(strName)) or (not cObject) then
        return false
    end

    -- Check updater.
    if bUpdate and (not cObject.Update) then
    	DLogWarn("Lua class \"" .. cObject:GetType() .. "\" used \"Update\" without providing a function!")
    end

    if bLateUpdate and (not cObject.LateUpdate) then
    	DLogWarn("Lua class \"" .. cObject:GetType() .. "\" used \"LateUpdate\" without providing a function!")
    end

    if bFixedUpdate and (not cObject.FixedUpdate) then
    	DLogWarn("Lua class \"" .. cObject:GetType() .. "\" used \"FixedUpdate\" without providing a function!")
    end

    if bLiteUpdate and (not cObject.LiteUpdate) then
    	DLogWarn("Lua class \"" .. cObject:GetType() .. "\" used \"LiteUpdate\" without providing a function!")
    end

    local aObjList = self.m_aObjectList
    if aObjList[strName] then
        return true
    end

    local cUpdateNode = YwUpdateNode(strName, cObject, bUpdate, bLateUpdate, bFixedUpdate, bLiteUpdate)
    table.insert(self.m_aObjectListInOrder, cUpdateNode)
    aObjList[strName] = #self.m_aObjectListInOrder

    return true
end

-- Unregister a object away from updater by name.
function YwMonoBehaviourUpdater:UnregisterUpdate(strName)
	--print("YwMonoBehaviourUpdater:UnregisterUpdate", strName)

	if (not strName) or (0 == str_len(strName)) then
        return false
    end

    -- Get the index by name.
    local aObjList = self.m_aObjectList
    local nObjectIdx = aObjList[strName]
    if not nObjectIdx then
        return false
    end

    -- Remove the update node.
    local aOrderObjList = self.m_aObjectListInOrder
    table.remove(aOrderObjList, nObjectIdx)
    aObjList[strName] = nil

    -- Update index.
    for i = nObjectIdx, #aOrderObjList do
        aObjList[aOrderObjList[i].m_strName] = i
    end

    return true
end

-- Set the updater of the object is active or not.
function YwMonoBehaviourUpdater:SetActive(strName, bActive)
	--print("YwMonoBehaviourUpdater:SetActive", strName, bActive)

	if (not strName) or (0 == str_len(strName)) then
        return
    end

    local nObjectIdx = self.m_aObjectList[strName]
    if not nObjectIdx then
        return
    end

    self.m_aObjectListInOrder[nObjectIdx].m_bActive = bActive
end

-- Static function.
-- Get the instance.
function YwMonoBehaviourUpdater.Instance()
    --print("YwMonoBehaviourUpdater:Instance")

    -- Check
    if not YwMonoBehaviourUpdater.m_cInstance then
        YwMonoBehaviourUpdater.new()
    end

    return YwMonoBehaviourUpdater.m_cInstance
end

-- Return this class.
return YwMonoBehaviourUpdater
