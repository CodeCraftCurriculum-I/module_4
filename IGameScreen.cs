namespace Adventure
{
    public interface IGameScreen
    {
        public abstract void Init();
        public abstract void Input();
        public abstract void Update();
        public abstract void Draw();

        public Action<Type> OnExitScreen { get; set; }

    }
}