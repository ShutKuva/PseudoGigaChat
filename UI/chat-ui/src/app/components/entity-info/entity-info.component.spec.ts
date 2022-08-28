import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EntityInfoComponent } from './entity-info.component';

describe('EntityInfoComponent', () => {
  let component: EntityInfoComponent;
  let fixture: ComponentFixture<EntityInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EntityInfoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EntityInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
