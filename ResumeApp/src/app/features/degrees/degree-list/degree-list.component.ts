import { Component, OnInit } from '@angular/core';
import { DegreeService } from '../../../core/Services/degree.service';
import { Degree } from '../../../core/Models/degree.model';
import { MatTableModule } from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-degree-list',
  imports: [CommonModule,MatTableModule, MatButtonModule],
  templateUrl: './degree-list.component.html',
  styleUrl: './degree-list.component.css'
})
export class DegreeListComponent implements OnInit {
  degrees: Degree[] = [];
  displayedColumns = ['id', 'name', 'actions'];

  constructor(private degreeService: DegreeService, private router: Router) {}

  ngOnInit() {
    this.loadDegrees();
  }

  loadDegrees() {
    this.degreeService.getAll().subscribe(data => this.degrees = data);
  }

  delete(id: number) {
    this.degreeService.delete(id).subscribe(() => this.loadDegrees());
  }

  openForm() {
    this.router.navigate(['/degrees/new']);
  }

  edit(id: number) {
    this.router.navigate(['/degrees/edit', id]);
  }
}
