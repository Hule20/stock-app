import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-details-btn',
  templateUrl: './details-btn.component.html',
  styleUrls: ['./details-btn.component.css']
})
export class DetailsBtnComponent {
  @Input() model: any;
  @Input() routePath: any;
}
