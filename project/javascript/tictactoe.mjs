import * as readline from 'node:readline';

/*
Information on ANSI
https://gist.github.com/fnky/458719343aabd01cfb17a3a4f7296797
https://www.youtube.com/watch?v=B20ebtwcIAY
https://www.lihaoyi.com/post/BuildyourownCommandLinewithANSIescapecodes.html
*/
const ESC = '\x1b';
const CSI = ESC + '[';
const CURSOR_UP = CSI + 'A';
const CURSOR_DOWN = CSI + 'B';
const CURSOR_RIGHT = CSI + 'C';
const CURSOR_LEFT = CSI + 'D';
const CLEAR_SCREEN = CSI + '2J';
const CURSOR_HOME = CSI + '1;1H';
const SAVE_CURSOR = ESC + '7';
const RESTORE_CURSOR = ESC + '8';
const moveCursorTo = (row, col) => CSI + row + ';' + col + 'H';
const BELL = '\x07';
                               
const DrawGameBoard = (offRow,offCol) => `${CURSOR_DOWN.repeat(offRow-1)}
${CURSOR_RIGHT.repeat(offCol)}╔═══╦═══╦═══╗
${CURSOR_RIGHT.repeat(offCol)}║   ║   ║   ║
${CURSOR_RIGHT.repeat(offCol)}╠═══╬═══╬═══╣
${CURSOR_RIGHT.repeat(offCol)}║   ║   ║   ║
${CURSOR_RIGHT.repeat(offCol)}╠═══╬═══╬═══╣
${CURSOR_RIGHT.repeat(offCol)}║   ║   ║   ║
${CURSOR_RIGHT.repeat(offCol)}╚═══╩═══╩═══╝`;

let board = [[0, 0, 0], [0, 0, 0], [0, 0, 0]];

const offsetRow = Math.round((process.stdout.rows -7)*0.5)
const offsetCol = Math.round((process.stdout.columns -13)*0.5)
const posistion = {row:1,col:1}
let currentPlayer = 1;
const token = () => (currentPlayer === 1 ? 'X' : 'O');
const color = () => (currentPlayer === 1 ? '\u001b[31m' : '\u001b[36m');
const boardToWorldCordinate = (row, col) => {
    return {row:((row * 2) + offsetRow + 2), col:((col * 4) + offsetCol)+3}
};

process.stdout.write(CLEAR_SCREEN);
process.stdout.write(CURSOR_HOME);  
process.stdout.write(`${CURSOR_RIGHT.repeat(offsetCol)}${CURSOR_DOWN.repeat(offsetRow-2)}Let's play Tic Tac Toe!\n`);
process.stdout.write(CURSOR_HOME);  
process.stdout.write(DrawGameBoard(offsetRow, offsetCol));
const startCoordinates = boardToWorldCordinate(posistion.row, posistion.col);

process.stdout.write(moveCursorTo(startCoordinates.row, startCoordinates.col));

readline.emitKeypressEvents(process.stdin);
if (process.stdin.isTTY) {
    process.stdin.setRawMode(true);
}


process.stdin.on("keypress", (str, key) => {

    if (key.name === 'x' || key.name === 'o' || key.name === 'X' || key.name === 'O' || key.name === 'space') {
        if (board[posistion.row][posistion.col] === 0) {
            process.stdout.write(`${SAVE_CURSOR}${color()}${token()}${RESTORE_CURSOR}`);
            board[posistion.row][posistion.col] = currentPlayer;
            currentPlayer *= -1;
        }
    }

    if (key.name === "q") {
        process.stdout.write(CLEAR_SCREEN+CURSOR_HOME);
        process.exit(0);
    }

    if (key.name === "up") {
        if (posistion.row - 1 >= 0) {
            posistion.row--;
            process.stdout.write(CURSOR_UP.repeat(2));
        }
    }

    if (key.name === "down") {
        if (posistion.row + 1 <= 2) {
            posistion.row++;
            process.stdout.write(CURSOR_DOWN.repeat(2));
        }
    }

    if (key.name === "left") {
        if(posistion.col-1 >= 0) {
            posistion.col--;
            process.stdout.write(CURSOR_LEFT.repeat(4));
        }
    }

    if (key.name === "right") {
        if(posistion.col+1 <= 2) {
            posistion.col++;
            process.stdout.write(CURSOR_RIGHT.repeat(4));
        }
    }
});

