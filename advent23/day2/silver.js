import { readFileSync } from "fs";

const readFileLines = (filename) =>
  readFileSync(filename).toString("UTF8").split("\n");

let lines = readFileLines("input.txt");

const limitRed = 12;
const limitGreen = 13;
const limitBlue = 14;
let sum = 0;

lines.map((line) => {
  const gameNumber = line.split(" ")[1].split(":")[0];
  const draws = line.split(": ")[1].split("\r")[0].split("; ");
  let exceededLimit = false;

  let number = 0;
  draws.map((draw) => {
    for (const index in draw) {
      const ascii = draw.charCodeAt(index);
      if (ascii >= 48 && ascii <= 57) {
        number = number * 10 + Number(draw[index]);
      } else {
        switch (draw[Number(index) + 1]) {
          case "r": {
            if (number > limitRed) exceededLimit = true;
            break;
          }
          case "b": {
            if (number > limitBlue) exceededLimit = true;
            break;
          }
          case "g": {
            if (number > limitGreen) exceededLimit = true;
            break;
          }
        }
        number = 0;
      }
    }
  });

  if (!exceededLimit) sum += Number(gameNumber);

  exceededLimit = false;
});

console.log(sum);
