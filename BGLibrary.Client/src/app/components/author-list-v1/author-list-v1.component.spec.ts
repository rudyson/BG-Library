import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorListV1Component } from './author-list-v1.component';

describe('AuthorListV1Component', () => {
  let component: AuthorListV1Component;
  let fixture: ComponentFixture<AuthorListV1Component>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AuthorListV1Component]
    });
    fixture = TestBed.createComponent(AuthorListV1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
