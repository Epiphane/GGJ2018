const yaml = require('js-yaml');
const fs   = require('fs');

const TEMPLATE = 'Assets/Art/Tiles/PNGs/tilesheet_complete.png.meta';
const TEMPLATE_SPRITES = [];
// tilesheet_complete_108 .. 116
// tilesheet_complete_135 .. 143
// ..
// tilesheet_complete_243 .. 251
var complete_lines = fs.readFileSync(TEMPLATE, 'utf8').split('\n');

const DESTINATION = 'Assets/Art/Tiles/PNGs/tileset2.png.meta';
const DESTINATION_SPRITES = [];
var destination_lines = fs.readFileSync(DESTINATION, 'utf8').split('\n');

for (var row = 0; row < 6; row ++) {
   for (var col = 0; col < 9; col ++) {
      var template = 'tilesheet_complete_' + (108 + row * 27 + col);
      TEMPLATE_SPRITES.push(template);
      DESTINATION_SPRITES.push('tileset2_' + (row * 9 + col));
   }
}

// Parse template
var physicsShapes = [];
var found = false;
var next = 0;
for (var l = 0; physicsShapes.length < TEMPLATE_SPRITES.length; l ++) {
   var line = complete_lines[l];
   var spriteName = TEMPLATE_SPRITES[next];

   if (found !== false) {
      // ??
      if (line.indexOf('tessellationDetail') >= 0) {
         physicsShapes.push(found);
         found = false;
         next ++;
      }
      else {
         found.push(line);
      }
   }
   else if (line.indexOf('name: ' + spriteName) >= 0) {
      found = [];

      while (complete_lines[l].indexOf('physicsShape') < 0) {
         l ++;
      }
   }
}

var RESULT = [];
next = 0;
for (var l = 0; l < destination_lines.length; l ++) {
   var line = destination_lines[l];
   var spriteName = DESTINATION_SPRITES[next];

   if (line.indexOf('name: ' + spriteName) >= 0) {
      RESULT.push(line);

      do {
         RESULT.push(destination_lines[++l]);
      } while (destination_lines[l].indexOf('physicsShape') < 0);
      RESULT = RESULT.concat(physicsShapes[next]);

      while (destination_lines[l].indexOf('tessellationDetail') < 0) {
         l ++;
      }
      RESULT.push(destination_lines[l]);
      next ++;
   }
   else {
      RESULT.push(line);
   }
}

fs.writeFileSync('output', RESULT.join('\n'));