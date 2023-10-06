namespace Adventure
{
    public interface IGameScreen
    {
        public Action<Type, object[]> OnExitScreen { get; set; }
        public void Init();
        public void Input();
        public void Update();
        public void Draw();



    }
}