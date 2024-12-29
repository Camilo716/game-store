import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { GameComment } from '../../core/models/game-comment';

@Component({
  selector: 'app-comment-tree',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './comment-tree.component.html',
  styleUrl: './comment-tree.component.css',
})
export class CommentTreeComponent {
  @Input() comments: GameComment[] = [];

  quoteComment(comment: GameComment) {
    console.log('Quote:', comment);
  }

  replyToComment(comment: GameComment) {
    console.log('Reply to:', comment);
  }

  banUser(comment: GameComment) {
    console.log('Ban user:', comment);
  }
}
