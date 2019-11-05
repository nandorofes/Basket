using System;

public struct FacebookScore
{
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Constantes
    // ---- ---- ---- ---- ---- ---- ---- ----
    private static readonly uint[] setMasksByLength =
        {
            0x00000000U, 0x00000001U, 0x00000003U, 0x00000007U, // 00-03
            0x0000000FU, 0x0000001FU, 0x0000003FU, 0x0000007FU, // 04-07
            0x000000FFU, 0x000001FFU, 0x000003FFU, 0x000007FFU, // 08-11
            0x00000FFFU, 0x00001FFFU, 0x00003FFFU, 0x00007FFFU, // 12-15
            0x0000FFFFU, 0x0001FFFFU, 0x0003FFFFU, 0x0007FFFFU, // 16-19
            0x000FFFFFU, 0x001FFFFFU, 0x003FFFFFU, 0x007FFFFFU, // 20-23
            0x00FFFFFFU, 0x01FFFFFFU, 0x03FFFFFFU, 0x07FFFFFFU, // 24-27
            0x0FFFFFFFU, 0x1FFFFFFFU, 0x3FFFFFFFU, 0x7FFFFFFFU, // 28-31
            0xFFFFFFFFU
        };
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Atributos
    // ---- ---- ---- ---- ---- ---- ---- ----
    private uint scoreValue;
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Propiedades
    // ---- ---- ---- ---- ---- ---- ---- ----
    public uint ScoreValue
    {
        get { return this.scoreValue; }
        set { this.scoreValue = value; }
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Constructores
    // ---- ---- ---- ---- ---- ---- ---- ----
    public FacebookScore(uint value)
    {
        this.scoreValue = value;
    }
    
    // ---- ---- ---- ---- ---- ---- ---- ----
    // Métodos
    // ---- ---- ---- ---- ---- ---- ---- ----
    public int GetBytes(int fromBit, int bitLength)
    {
        return (int)((this.scoreValue >> fromBit) & FacebookScore.setMasksByLength[bitLength]);
    }
    
    public void SetBytes(int fromBit, int bitLength, int value)
    {
        uint clearMask = ~(FacebookScore.setMasksByLength[bitLength] << fromBit);
        uint setMask = ((uint)value & FacebookScore.setMasksByLength[bitLength]) << fromBit;
        
        this.scoreValue &= clearMask;
        this.scoreValue |= setMask;
    }
    
}