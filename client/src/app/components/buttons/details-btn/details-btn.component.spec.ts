import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailsBtnComponent } from './details-btn.component';

describe('DetailsBtnComponent', () => {
  let component: DetailsBtnComponent;
  let fixture: ComponentFixture<DetailsBtnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetailsBtnComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetailsBtnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
