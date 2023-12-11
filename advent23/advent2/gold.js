import { readFileSync } from "fs";

const readFileLines = (filename) =>
  readFileSync(filename).toString("UTF8").split("\n");

let lines = readFileLines("input.txt");

let sum = 0;

lines.map((line) => {
  const gameNumber = line.split(" ")[1].split(":")[0];
  const draws = line.split(": ")[1].split("\r")[0].split("; ");
  let minRed = 0;
  let minGreen = 0;
  let minBlue = 0;

  let number = 0;
  draws.map((draw) => {
    for (const index in draw) {
      const ascii = draw.charCodeAt(index);
      if (ascii >= 48 && ascii <= 57) {
        number = number * 10 + Number(draw[index]);
      } else if (number) {
        switch (draw[Number(index) + 1]) {
          case "r": {
            if (minRed < number) minRed = number;
            break;
          }
          case "b": {
            if (minBlue < number) minBlue = number;
            break;
          }
          case "g": {
            if (minGreen < number) minGreen = number;
            break;
          }
        }
        number = 0;
      }
    }
  });
  const power = minRed * minBlue * minGreen;
  sum += power;
});

console.log(sum);
