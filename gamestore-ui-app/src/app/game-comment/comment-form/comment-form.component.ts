import { Component, inject, OnInit } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatOptionModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { CommentFormData } from './comment-form-data';
import { CommentService } from '../../core/services/comment.service';
import { GameComment } from '../../core/models/game-comment';

@Component({
  selector: 'app-comment-form',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatDialogContent,
    MatDialogActions,
    MatInputModule,
    ReactiveFormsModule,
    MatOptionModule,
    MatSelectModule,
    CommonModule,
  ],
  templateUrl: './comment-form.component.html',
  styleUrl: './comment-form.component.css',
})
export class CommentFormComponent implements OnInit {
  readonly dialogRef = inject(MatDialogRef<CommentFormComponent>);
  readonly commentFormData = inject<CommentFormData>(MAT_DIALOG_DATA);
  comment: GameComment;
  gameKey: string;

  form: FormGroup = this.formBuilder.group({
    body: [null, Validators.required],
  });

  constructor(
    private commentService: CommentService,
    private formBuilder: FormBuilder
  ) {
    this.comment = this.commentFormData.comment;
    this.gameKey = this.commentFormData.gameKey;
  }

  ngOnInit(): void {
    this.setForm();
  }

  setForm() {
    this.form.patchValue(this.comment);
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    const comment: GameComment = {
      ...this.comment,
      ...this.form.value,
    };

    console.log(comment);

    this.commentService
      .addComment(comment, this.gameKey)
      .subscribe(() => this.dialogRef.close());
  }
}
