import { Component, OnInit } from '@angular/core';
import { DegreeService } from '../../../core/Services/degree.service';
import { Router, RouterModule, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Degree } from '../../../core/Models/degree.model';
import { CommonModule } from '@angular/common';
import { MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-degree-form',
  imports: [CommonModule, FormsModule, MatOptionModule, MatFormFieldModule,
    MatInputModule, ReactiveFormsModule, RouterModule, MatSelectModule,],
  templateUrl: './degree-form.component.html',
  styleUrl: './degree-form.component.css'
})

export class DegreeFormComponent implements OnInit {
  form!: FormGroup;
  isEditMode = false;
  degreeId?: number;

  constructor(
    private fb: FormBuilder, 
    private degreeService: DegreeService, 
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initForm();
    
    // Check if we're in edit mode
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.degreeId = +id;
      this.loadDegree(this.degreeId);
    }
  }

  private initForm(): void {
    this.form = this.fb.group({
      name: ['', Validators.required]
    });
  }

  private loadDegree(id: number): void {
    this.degreeService.getById(id).subscribe(degree => {
      this.form.patchValue({
        name: degree.name
      });
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      const degree: Degree = this.form.value;
      
      if (this.isEditMode && this.degreeId) {
        this.degreeService.update(this.degreeId, degree)
          .subscribe(() => this.router.navigate(['/degrees']));
      } else {
        this.degreeService.create(degree)
          .subscribe(() => this.router.navigate(['/degrees']));
      }
    }
  }
}