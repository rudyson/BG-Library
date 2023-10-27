import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookNewFormComponent } from './book-new-form.component';

describe('BookNewFormComponent', () => {
  let component: BookNewFormComponent;
  let fixture: ComponentFixture<BookNewFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BookNewFormComponent]
    });
    fixture = TestBed.createComponent(BookNewFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
