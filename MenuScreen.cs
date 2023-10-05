namespace Adventure
{
    public class MenuScreen : IGameScreen
    {
        public Action<Type> OnExitScreen { get; set; }
        public void Init() { }
        public void Input() { }
        public void Update() { }
        public void Draw() { }


    }
}