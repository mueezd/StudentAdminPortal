import { Component, OnInit, ViewChild } from '@angular/core';
import { StudentsComponent } from '../students.component';
import { StudentService } from '../student.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Student } from 'src/app/models/ui-models/student.model';
import { GenderService } from 'src/app/services/gender.service';
import { Gender } from 'src/app/models/ui-models/gender.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-view-student',
  templateUrl: './view-student.component.html',
  styleUrls: ['./view-student.component.css'],
})
export class ViewStudentComponent implements OnInit {
  studentId: string | null | undefined;
  student: Student = {
    id: '',
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    email: '',
    mobile: 0,
    genderId: '',
    profileImageUrl: '',
    gender: {
      id: '',
      description: '',
    },
    address: {
      id: '',
      physicalAddress: '',
      postalAddress: '',
    },
  };

  isNewStudent = false;
  header = '';
  displayProfileImageUrl = '';

  genderList: Gender[] = [];

  @ViewChild('studentDetsilsForm') studentDetsilsForm?: NgForm;

  constructor(
    private studentService: StudentService,
    private route: ActivatedRoute,
    private genderService: GenderService,
    private snackbar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.studentId = params.get('id');

      if (this.studentId) {
        // if the rount conain key work add

        if (this.studentId.toLowerCase() === 'Add'.toLowerCase()) {
          this.isNewStudent = true;
          this.header = 'Add New Student';
          this.setImage();
        } else {
          this.isNewStudent = false;
          this.header = 'Update Student';
          this.studentService.getStudentById(this.studentId).subscribe(
            (successResponse) => {
              this.student = successResponse;
              this.setImage();
            },
            (errorResponse) => {
              this.setImage();
            }
          );
        }
      }
    });

    this.genderService.getGenders().subscribe((successResponse) => {
      this.genderList = successResponse;
    });
  }

  onUpdate(): void {
    if(this.studentDetsilsForm?.form.valid){
      this.studentService.updateStudent(this.student.id, this.student).subscribe(
        (successResponse) => {
          this.snackbar.open('Student Update Successfully', undefined, {
            duration: 2000,
          });
        },
        (errorResponse) => {
          //Log It

          console.log(errorResponse);
        }
      );
    }
  }

  onDelete(): void {
    this.studentService.deleteStudent(this.student.id).subscribe(
      (successResponse) => {
        this.snackbar.open('Student Deleted Successfully', undefined, {
          duration: 2000,
        });

        setTimeout(() => {
          this.router.navigateByUrl('students');
        }, 2000);
      },
      (errorResponse) => {
        //log It
      }
    );
  }

  onAdd(): void {

    if(this.studentDetsilsForm?.form.valid){
      this.studentService.addStudent(this.student).subscribe(
        (successResponse) => {
          this.snackbar.open('Student Added Successfully', undefined, {
            duration: 2000,
          });
  
          setTimeout(() => {
            this.router.navigateByUrl(`students/${successResponse.id}`);
          }, 2000);
        },
        (errorResponse) => {
          console.log(errorResponse)
        }
      );
    }
    
  }

  private setImage(): void {
    if (this.student.profileImageUrl) {
      this.displayProfileImageUrl = this.studentService.getImagePath(this.student.profileImageUrl);
    } else {
      this.displayProfileImageUrl = '/assets/images/user.png';
    }
  }


  uploadImage(event: any): void{
    if(this.studentId){
      const file: File = event.target.files[0];
      this.studentService.uploadImage(this.studentId, file)
      .subscribe(
        (successResponse) => {
          this.student.profileImageUrl =  successResponse;
          this.setImage();
          this.snackbar.open('Profile Image Upload Successfully', undefined, {
            duration: 2000,
          });
        },
        (errorResponse) => {

        }
      );
    }
  }
}
