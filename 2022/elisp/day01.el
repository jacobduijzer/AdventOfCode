(require 'ert)
(load-file "functions.el")

(defun my-file-contents (filename)
  "Return the contents of FILENAME."
  (with-temp-buffer
    (insert-file-contents filename)
    (buffer-string)))

(defun cals-per-elf (filename)
  (with-temp-buffer
    (progn (insert "((")
           (insert-file-contents filename)
           (while (re-search-forward "^$" nil t)
             (replace-match ")(" nil nil))
           (end-of-buffer)(insert "))")
           (goto-char 0))
    (mapcar (lambda (food-carried)
              (cl-reduce #'+ food-carried))
            (read (current-buffer)))))

(defun aoc-day1-part1 ()
	(cl-reduce #'max (cals-per-elf "assets/day01-test.txt")))

(aoc-day1-part1)

(ert-deftest my-erttest-d1 ()
  "Tests for day 1"
  (let ((data '(1000 2000 3000 nil 4000 nil 5000 6000 nil 7000 8000 9000 nil 10000)))
    (should (equal 24000 (apply #'max (my-calories data)))))
  (should (equal 71934 (my-d1p1)))
  (should (equal 211447 (my-d1p2))))

(my-erttest-d1)
