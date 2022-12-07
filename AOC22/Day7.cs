using System.Text.Json;
using System.Text.RegularExpressions;

namespace AOC22
{
    public static class Day7
    {
        const string _inputFilePath = "../../../inputd7.txt";
        const string _inputLineSeparator = "\n";
        const char _commandChar = '$';
        const string _changeDirectoryCommandPrefix = "cd ";
        const string _changeDirectoryUpCommandSuffix = "..";
        const char _directorySeparator = '/';
        const char _spaceChar = ' ';
        const int _partOneContentSizeValue = 100000;
        const int _totalDeviceDiskSpace = 70000000;
        const int _diskSpaceRequiredForUpdate = 30000000;

        public static void Run()
        {
            var input = File.ReadAllText(_inputFilePath);
            var inputList = input.Split(_inputLineSeparator).Where(x => !string.IsNullOrEmpty(x)).ToList();

            var directoryList = new List<Directory>();
            var currentPath = string.Empty;
            var previousPath = string.Empty;

            foreach (var singleLine in inputList)
            {
                if (singleLine.StartsWith(_commandChar) && singleLine.Contains(_changeDirectoryCommandPrefix) && !singleLine.Contains(_changeDirectoryUpCommandSuffix))
                {
                    previousPath = currentPath;

                    currentPath = $"{currentPath}{_directorySeparator}{singleLine.Substring(singleLine.IndexOf(_changeDirectoryCommandPrefix) + _changeDirectoryCommandPrefix.Length)}";

                    if (directoryList.FirstOrDefault(x => x.Path == currentPath) == default)
                    {
                        var dir = new Directory
                        {
                            Path = currentPath,
                            ParentDirectoryPath = directoryList.FirstOrDefault(x => x.Path == previousPath)?.Path ?? string.Empty
                        };

                        directoryList.Add(dir);
                    }
                }

                if (singleLine.StartsWith(_commandChar) && singleLine.Contains(_changeDirectoryCommandPrefix) && singleLine.Contains(_changeDirectoryUpCommandSuffix))
                {
                    previousPath = currentPath;
                    currentPath = currentPath.Substring(0, currentPath.LastIndexOf(_directorySeparator));
                }

                if (Regex.IsMatch(singleLine, "^\\d"))
                {
                    var fileDiskSpace = int.Parse(singleLine.Substring(0, singleLine.IndexOf(_spaceChar)));
                    directoryList.First(x => x.Path == currentPath).ContentSize += fileDiskSpace;
                    PerformOnParents(directoryList.First(x => x.Path == currentPath).ParentDirectoryPath, fileDiskSpace);
                }
            }

            void PerformOnParents(string parentPath, int size)
            {
                var directory = directoryList.FirstOrDefault(x => x.Path == parentPath);
                if (directory != default)
                {
                    directory.ContentSize += size;
                    PerformOnParents(directory.ParentDirectoryPath, size);
                }
            }

            int resultPartOne = directoryList.Where(x => x.ContentSize <= _partOneContentSizeValue).Sum(x => x.ContentSize);

            Console.WriteLine($"D7 PT1: {resultPartOne}");

            int deviceAvailableDiskSpace = _totalDeviceDiskSpace - directoryList.First(x => string.IsNullOrEmpty(x.ParentDirectoryPath)).ContentSize;

            int diskSpaceToBeCleared = _diskSpaceRequiredForUpdate - deviceAvailableDiskSpace;

            int resultPartTwo = directoryList.Where(x => x.ContentSize >= diskSpaceToBeCleared).OrderBy(x => x.ContentSize).First().ContentSize;

            Console.WriteLine($"D7 PT2: {resultPartTwo}");
        }

        public class Directory
        {
            public string Path { get; set; }
            public string ParentDirectoryPath { get; set; }
            public int ContentSize { get; set; }
        }
    }
}
