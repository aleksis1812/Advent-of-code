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
    if (ascii >= 48 && ascii <= 57) {
      if (firstDigit == -1) firstDigit = line[index];
      lastDigit = line[index];
    }
  }

  if (lastDigit == -1) lastDigit = firstDigit;

  twoDigitNumbers.push(Number(firstDigit) * 10 + Number(lastDigit));
});

let sum = 0;
twoDigitNumbers.map((number) => (sum += number));

console.log(sum);
