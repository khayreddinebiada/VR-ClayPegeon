namespace game.lib
{
    public class MathF
    {
        public static float ResetAngle(float angle)
        {
            return (180 <= angle) ? angle - 360 : angle;
        }
    }
}
