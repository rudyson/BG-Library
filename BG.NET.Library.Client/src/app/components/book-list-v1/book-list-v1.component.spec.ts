import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookListV1Component } from './book-list-v1.component';

describe('BookListV1Component', () => {
  let component: BookListV1Component;
  let fixture: ComponentFixture<BookListV1Component>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BookListV1Component]
    });
    fixture = TestBed.createComponent(BookListV1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
