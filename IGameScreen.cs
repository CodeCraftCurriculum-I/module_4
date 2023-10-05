namespace Adventure
{
    public interface IGameScreen
    {
        public void Init();
        public void Input();
        public void Update();
        public void Draw();

        public Action<Type> OnExitScreen { get; set; }

    }
}