import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Degree } from '../../../core/Models/degree.model';
import { CandidateService } from '../../../core/Services/candidate.service';
import { DegreeService } from '../../../core/Services/degree.service';
import { Router, RouterModule, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-candidate-form',
  imports: [CommonModule, FormsModule, MatOptionModule, MatFormFieldModule,
    MatInputModule, ReactiveFormsModule, RouterModule, MatSelectModule,
],
  templateUrl: './candidate-form.component.html',
  styleUrl: './candidate-form.component.css',
})
export class CandidateFormComponent implements OnInit {
  form!: FormGroup;
  degrees: Degree[] = [];
  selectedFile?: File;
  isEditMode = false;
  candidateId?: number;

  constructor(
    private fb: FormBuilder,
    private candidateService: CandidateService,
    private degreeService: DegreeService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.loadDegrees();
    
    // Check if we're in edit mode
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.candidateId = +id;
      this.loadCandidate(this.candidateId);
    }
  }

  private initializeForm() {
    this.form = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      mobile: ['', [Validators.pattern(/^\d{10}$/)]],
      degreeId: [null],
    });
  }

  private loadCandidate(id: number) {
    this.candidateService.getById(id).subscribe(candidate => {
      this.form.patchValue({
        firstName: candidate.firstName,
        lastName: candidate.lastName,
        email: candidate.email,
        mobile: candidate.mobile,
        degreeId: candidate.degreeId
      });
    });
  }

  loadDegrees() {
    this.degreeService.getAll().subscribe(data => this.degrees = data);
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  onSubmit() {
    if (this.form.valid) {
      if (this.isEditMode && this.candidateId) {
        this.candidateService.update(this.candidateId, this.form.value)
          .subscribe(() => this.router.navigate(['/candidates']));
      } else {
        this.candidateService.create(this.form.value, this.selectedFile)
          .subscribe(() => this.router.navigate(['/candidates']));
      }
    }
  }
}
