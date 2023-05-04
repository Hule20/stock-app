import { Component } from '@angular/core';

@Component({
  selector: 'app-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.css'],
})
export class BannerComponent {
  public name: string = 'stockify';

  logoAnimation() {
    const letters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'.toLowerCase();
    const nameChar = this.name.split('');
    let interval: number = 0;
    let iteration = 0;
    
    clearInterval(interval);
    interval = window.setInterval(() => {
      this.name = nameChar
        .map((letter: string, index: number) => {
          if (index < iteration) {
            return nameChar.at(index);
          }

          return letters[Math.floor(Math.random() * 26)];
        })
        .join('');

      if (iteration >= this.name.length) {
        clearInterval(interval);
      }

      iteration += 1 / 3;
    }, 30);
  }
}
