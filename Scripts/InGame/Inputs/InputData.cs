namespace Unity1week202306.InGame.Inputs
{
    public struct InputData
    {
        public float Horizontal;
        public float Vertical;
        public bool IsJump;

        public static InputData Neutral => new InputData()
        {
            Horizontal = 0,
            Vertical = 0,
            IsJump = false,
        };
    }
}