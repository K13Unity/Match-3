namespace Assets.Scripts
{
    public class InputState: State<GameController>
    {
        public InputState(GameController core) : base(core) { }

        public override void OnEnter()
        {
            if(_core.InputProcess == null) _core.InputProcess = new InputProcess(_core);
            
            if(_core.InputProcess.isRunning == false) _core.InputProcess.Start();

            _core.InputProcess.onMatches += OnMatches;
        }

        private void OnMatches()
        {
            _core.InputProcess.onMatches -= OnMatches;
            ChangeState(new RemoveItemsState(_core));
        }
    }
}
