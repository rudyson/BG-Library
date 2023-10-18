import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookTableV1Component } from './book-table-v1.component';

describe('BookTableV1Component', () => {
  let component: BookTableV1Component;
  let fixture: ComponentFixture<BookTableV1Component>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BookTableV1Component]
    });
    fixture = TestBed.createComponent(BookTableV1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
