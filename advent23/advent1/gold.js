import { readFileSync } from "fs";

const readFileLines = (filename) =>
  readFileSync(filename).toString("UTF8").split("\n");

// ['line1', 'line2', 'line3']
let lines = readFileLines("advent1.txt");

const twoDigitNumbers = [];

lines.map((line) => {
  let firstDigit = -1;
  let lastDigit = -1;

  for (const index in line) {
    const ascii = line.charCodeAt(index);
    const letter = line[index];

    if (ascii >= 48 && ascii <= 57) {
      if (firstDigit == -1) firstDigit = letter;
      lastDigit = letter;
      continue;
    }

    switch (letter) {
      case "o": {
        if (line[Number(index) + 1] == "n" && line[Number(index) + 2] == "e") {
          if (firstDigit == -1) firstDigit = 1;
          lastDigit = 1;
        }
        break;
      }
      case "t": {
        if (line[Number(index) + 1] == "w" && line[Number(index) + 2] == "o") {
          if (firstDigit == -1) firstDigit = 2;
          lastDigit = 2;
        }
        if (
          line[Number(index) + 1] == "h" &&
          line[Number(index) + 2] == "r" &&
          line[Number(index) + 3] == "e" &&
          line[Number(index) + 4] == "e"
        ) {
          if (firstDigit == -1) firstDigit = 3;
          lastDigit = 3;
        }
        break;
      }
      case "f": {
        if (
          line[Number(index) + 1] == "o" &&
          line[Number(index) + 2] == "u" &&
          line[Number(index) + 3] == "r"
        ) {
          if (firstDigit == -1) firstDigit = 4;
          lastDigit = 4;
        }
        if (
          line[Number(index) + 1] == "i" &&
          line[Number(index) + 2] == "v" &&
          line[Number(index) + 3] == "e"
        ) {
          if (firstDigit == -1) firstDigit = 5;
          lastDigit = 5;
        }
        break;
      }
      case "s": {
        if (line[Number(index) + 1] == "i" && line[Number(index) + 2] == "x") {
          if (firstDigit == -1) firstDigit = 6;
          lastDigit = 6;
        }
        if (
          line[Number(index) + 1] == "e" &&
          line[Number(index) + 2] == "v" &&
          line[Number(index) + 3] == "e" &&
          line[Number(index) + 4] == "n"
        ) {
          if (firstDigit == -1) firstDigit = 7;
          lastDigit = 7;
        }
        break;
      }
      case "e": {
        if (
          line[Number(index) + 1] == "i" &&
          line[Number(index) + 2] == "g" &&
          line[Number(index) + 3] == "h" &&
          line[Number(index) + 4] == "t"
        ) {
          if (firstDigit == -1) firstDigit = 8;
          lastDigit = 8;
        }
        break;
      }
      case "n": {
        if (
          line[Number(index) + 1] == "i" &&
          line[Number(index) + 2] == "n" &&
          line[Number(index) + 3] == "e"
        ) {
          if (firstDigit == -1) firstDigit = 9;
          lastDigit = 9;
        }
        break;
      }
    }
  }

  if (lastDigit == -1) lastDigit = firstDigit;

  twoDigitNumbers.push(Number(firstDigit) * 10 + Number(lastDigit));
});

let sum = 0;
twoDigitNumbers.map((number) => (sum += number));

console.log(sum);
