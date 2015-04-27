Feature: BasicScanning
    You work for a bank, which has recently purchased an
    ingenious machine to assist in reading letters and faxes
    sent in by branch offices. The machine scans the paper
    documents, and produces a file with a number of entries
    which each look like this:
        _  _     _  _  _  _  _
      | _| _||_||_ |_   ||_||_|
      ||_  _|  | _||_|  ||_| _|
                           
    Each entry is 4 lines long, and each line has 27 characters.
    The first 3 lines of each entry contain an account number
    written using pipes and underscores, and the fourth line
    is blank. Each account number should have 9 digits, all of
    which should be in the range 0-9.
    A normal file contains around 500 entries.

@scan
Scenario: Scan line with all digits
    Given the machine produces 1 valid entry(ies)
    When I scan
    Then the result should 1 valid bank account number(s)

@scan
Scenario: Scan lines with repeating number sequences
	Given the machine produces 10 valid entry(ies)
	When I scan
    Then the result should 10 valid bank account number(s)
