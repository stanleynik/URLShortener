import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UrlShortenerService } from '../services/url-shortener.service';

@Component({
  selector: 'app-url-shortener',
  templateUrl: './url-shortener.component.html',
  styleUrls: ['./url-shortener.component.css']
})
export class UrlShortenerComponent implements OnInit {

  short_url: string;
  form: FormGroup = new FormGroup({
    url: new FormControl('')
  });
  submitted = false;

  constructor(private fb: FormBuilder, private urlShortenerService:UrlShortenerService) {
    
  }

  ngOnInit(): void {
    this.createForm();
  }

  // f to access form controls (form.controls) Ex: f.url
  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  createForm() {
    // Regex to validate URL
    // https://mathiasbynens.be/demo/url-regex
    const reg = new RegExp('^(https?:\\/\\/)?'+ // protocol
    '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|' + // domain name
      '((\\d{1,3}\\.){3}\\d{1,3}))' + // OR ip (v4) address
      '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*' + // port and path
      '(\\?[;&a-z\\d%_.~+=-]*)?' + // query string
      '(\\#[-a-z\\d_]*)?$', 'i');  // fragment locator
    // Create form
    this.form = this.fb.group({
      url: ['', [Validators.required, Validators.pattern(reg)]],
    });
  }

  shortenURL() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }

    this.urlShortenerService.addURL(this.form.value.url).subscribe({
      next: result => {
        this.short_url = result;
      },
      error: err => alert(err.message)
    });

  }
}
