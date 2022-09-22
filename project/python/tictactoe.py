import sys,os,keyboard

"""
Information on ANSI
https://gist.github.com/fnky/458719343aabd01cfb17a3a4f7296797
https://www.youtube.com/watch?v=B20ebtwcIAY
https://www.lihaoyi.com/post/BuildyourownCommandLinewithANSIescapecodes.html
"""
ESC = '\x1b'
CSI = ESC + '['
CURSOR_UP = CSI + 'A'
CURSOR_DOWN = CSI + 'B'
CURSOR_RIGHT = CSI + 'C'
CURSOR_LEFT = CSI + 'D'
CLEAR_SCREEN = CSI + '2J'
CURSOR_HOME = CSI + '0;0H'
SAVE_CURSOR = ESC + '7'
RESTORE_CURSOR = ESC + '8'
def moveCursorTo(row, col): return f'{CSI}{row};{col}H'


def DrawGameBoard(offRow, offCol): return f'''{CURSOR_DOWN * (offRow-1)}
{CURSOR_RIGHT * offCol}╔═══╦═══╦═══╗
{CURSOR_RIGHT * offCol}║   ║   ║   ║
{CURSOR_RIGHT * offCol}╠═══╬═══╬═══╣
{CURSOR_RIGHT * offCol}║   ║   ║   ║
{CURSOR_RIGHT * offCol}╠═══╬═══╬═══╣
{CURSOR_RIGHT * offCol}║   ║   ║   ║
{CURSOR_RIGHT * offCol}╚═══╩═══╩═══╝'''


board = [[0, 0, 0], [0, 0, 0], [0, 0, 0]]

terminalSize = os.get_terminal_size()

offsetRow = round((terminalSize.lines - 7)*0.5)
offsetCol = round((terminalSize.columns - 13)*0.5)
posistion = {"row": 1, "col": 1}
currentPlayer = 1
def token(): return 'X' if currentPlayer == 1 else 'O'
def color(): return '\u001b[31m' if currentPlayer == 1 else '\u001b[36m'


def boardToWorldCordinate(row, col): return {"row": (
    (row * 2) + offsetRow + 2), "col": ((col * 4) + offsetCol)+3}


sys.stdout.write(CLEAR_SCREEN)
sys.stdout.write(CURSOR_HOME)
sys.stdout.write(f'{CURSOR_RIGHT * offsetCol }{CURSOR_DOWN * (offsetRow-2)}Let\'s play Tic Tac Toe!\n')
sys.stdout.write(CURSOR_HOME)
sys.stdout.write(DrawGameBoard(offsetRow, offsetCol))

startCoordinates = boardToWorldCordinate(posistion['row'], posistion['col'])
sys.stdout.write(moveCursorTo(startCoordinates['row'], startCoordinates['col']))

while True:
    keyName = keyboard.read_key()

    if (keyName ==  'x' or keyName ==  'o' or keyName ==  'X' or keyName ==  'O' or keyName ==  'space') :
        if (board[posistion["row"]][posistion["col"]] ==  0) :
            sys.stdout.write(f'{SAVE_CURSOR}{color()}{token()}{RESTORE_CURSOR}')
            board[posistion["row"]][posistion["col"]] = currentPlayer
            currentPlayer = currentPlayer * -1

    if keyName == "q" :
        sys.stdout.write(CLEAR_SCREEN+CURSOR_HOME)
        sys.exit(0)
    
    if keyName == "up" :
        if posistion["row"] - 1 >= 0 :
            posistion["row"] -= 1
            sys.stdout.write(CURSOR_UP * 2)
       
    if (keyName == "down") :
        if (posistion["row"] + 1 <= 2) :
            posistion["row"] += 1
            sys.stdout.write(CURSOR_DOWN * 2)
        
    if (keyName == "left") :
        if posistion["col"] -1 >= 0 :
            posistion["col"] -= 1
            sys.stdout.write(CURSOR_LEFT * 4)
    
    if (keyName == "right") :
        if(posistion["col"]+1 <= 2) :
            posistion["col"] += 1;
            sys.stdout.write(CURSOR_RIGHT * 4);
        
    