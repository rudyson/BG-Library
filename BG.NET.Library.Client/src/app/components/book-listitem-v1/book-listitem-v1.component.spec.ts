import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookListitemV1Component } from './book-listitem-v1.component';

describe('BookListitemV1Component', () => {
  let component: BookListitemV1Component;
  let fixture: ComponentFixture<BookListitemV1Component>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BookListitemV1Component]
    });
    fixture = TestBed.createComponent(BookListitemV1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
