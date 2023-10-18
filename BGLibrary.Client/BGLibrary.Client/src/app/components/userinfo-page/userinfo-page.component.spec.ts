import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserinfoPageComponent } from './userinfo-page.component';

describe('UserinfoPageComponent', () => {
  let component: UserinfoPageComponent;
  let fixture: ComponentFixture<UserinfoPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserinfoPageComponent]
    });
    fixture = TestBed.createComponent(UserinfoPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
