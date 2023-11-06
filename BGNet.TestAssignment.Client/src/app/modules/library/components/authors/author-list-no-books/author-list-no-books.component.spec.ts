import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorListNoBooksComponent } from './author-list-no-books.component';

describe('AuthorListNoBooksComponent', () => {
  let component: AuthorListNoBooksComponent;
  let fixture: ComponentFixture<AuthorListNoBooksComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AuthorListNoBooksComponent]
    });
    fixture = TestBed.createComponent(AuthorListNoBooksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
