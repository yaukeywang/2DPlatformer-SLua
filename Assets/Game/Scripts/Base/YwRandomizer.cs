/**
 * Randomizer generation class.
 *
 * @filename  YwRandomizer.cs
 * @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
 * @license   The MIT License (MIT)
 * @author    Yaukey
 * @date      2015-08-31
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Randomizer generation template class.
public class YwRandomizer<T>
{
    // Current list.
    private List<T> m_cCurrent = null;

    // Next list.
    private List<T> m_cNext = null;

    /**
     * Constructor.
     * 
     * @param int nInitSize - Init capacity size.
     * @return void.
     */
    public YwRandomizer(int nInitSize = 2)
    {
        nInitSize = (nInitSize <= 1) ? 2 : nInitSize;
        m_cCurrent = new List<T>(nInitSize - 1);
        m_cNext = new List<T>(nInitSize - 1);
    }

    /**
     * Add item.
     * 
     * @param T x - The item.
     * @return void.
     */
    public void Add(T x)
    {
        m_cCurrent.Add(x);
    }

    /**
     * Get a random item.
     * 
     * @param void.
     * @return T - The item.
     */
    public T Get()
    {
        if (m_cCurrent.Count > 1)
        {
            int nIdx = Random.Range(0, m_cCurrent.Count);
            T t = m_cCurrent[nIdx];
            m_cCurrent.RemoveAt(nIdx);
            m_cNext.Add(t);

            return t;
        }

        T result = m_cCurrent[0];
        if (m_cNext.Count > 0)
        {
            List<T> cTmp = m_cCurrent;
            m_cCurrent = m_cNext;
            m_cNext = cTmp;
        }

        return result;
    }

    /**
     * If contains a random item.
     * 
     * @param T x - The item.
     * @return bool - true if contains the item, otherwise false.
     */
    public bool Contains(T x)
    {
        return m_cCurrent.Contains(x) || m_cNext.Contains(x);
    }

    /**
     * Clear all the item.
     * 
     * @param void.
     * @return void.
     */
    public void Clear()
    {
        m_cCurrent.Clear();
        m_cNext.Clear();
    }

    // Get all of the item count.
    public int Count
    {
        get
        {
            return m_cCurrent.Count + m_cNext.Count;
        }
    }
}
