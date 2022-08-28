import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SomeoneMessageComponent } from './someone-message.component';

describe('SomeoneMessageComponent', () => {
  let component: SomeoneMessageComponent;
  let fixture: ComponentFixture<SomeoneMessageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SomeoneMessageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SomeoneMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
