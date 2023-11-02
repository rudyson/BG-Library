import {Component, OnInit} from '@angular/core';
import {GenericPaginationModel} from "../../special/genericPagination.model";
import {AuthorFullDto} from "../../special/entities";
import {ActivatedRoute, Router} from "@angular/router";
import {PageEvent} from "@angular/material/paginator";
import {AuthorsService} from "../../services/authors/authors.service";

@Component({
  selector: 'app-author-list-v1',
  templateUrl: './author-list-v1.component.html',
  styleUrls: ['./author-list-v1.component.css']
})
export class AuthorListV1Component implements OnInit{
  public authors?: GenericPaginationModel<AuthorFullDto>;
  constructor(private authorsService: AuthorsService, private router: Router, private route: ActivatedRoute) {

  }
  ngOnInit(): void {
    this.handlePaginationEvent(undefined);
  }
  handlePaginationEvent($event?: PageEvent) {
    this.authorsService.getAllAuthors(
      $event?.pageIndex===undefined ? Number(this.route.snapshot.paramMap.get('id') ?? 1) : $event?.pageIndex + 1,
      $event?.pageSize
    )
      .subscribe({
        next: (authors) => {
          this.authors = undefined;
          this.authors = authors;
          console.log(authors);
          window.history.replaceState({},'',`/authors/${this.authors.page}`)
        },
        error: (response) =>{
          console.log(response)
          //this.router.navigate(["/authors/1"]);
          //this.handlePaginationEvent(undefined);
        }
      })
  }
  deleteAuthor(id: number){
    if (confirm("Do you want to delete author with id "+id+"?")){
      this.authorsService.deleteAuthor(id).subscribe({
        next: ()=>{
          this.handlePaginationEvent(undefined)
        },
        error:(response)=>{
          console.log("Deletion error")
          console.log(response)
        }
      })
    }
  }
}
