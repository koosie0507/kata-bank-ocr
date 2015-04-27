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
Scenario: Scan one line
    Given the machine produces one entry
    And the entry has exactly 4 lines
    And each line has exactly 27 characters
    When I scan the entry
    Then the result should a bank account number
    And the account number should be 9 digits long
